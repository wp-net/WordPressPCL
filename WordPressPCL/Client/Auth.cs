using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;


/// <summary>
/// Client class for interaction with Auth endpoints of WP REST API
/// </summary>
public class Auth
{

    private const string JwtPath = "jwt-auth/v1/";

    /// <summary>
    /// Authentication Method
    /// </summary>
    public AuthMethod AuthMethod { get; private set; } = AuthMethod.Bearer;

    /// <summary>
    /// JWT Plugin
    /// </summary>
    public JWTPlugin JWTPlugin { get; private set; } = JWTPlugin.JWTAuthByEnriqueChavez;

    /// <summary>
    /// Username for Basic Authentication
    /// </summary>
    public string Username { get; private set; } = string.Empty;

    /// <summary>
    /// Helper for HTTP requests
    /// </summary>
    private readonly HttpHelper _httpHelper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Auth(HttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    /// <summary>
    /// Sets up the Auth Client to use Bearer Authentication with specified JWTPlugin
    /// </summary>
    /// <param name="jwtPlugin">JWT Plugin to use for Bearer Authentication</param>
    public void UseBearerAuth(JWTPlugin jwtPlugin)
    {
        AuthMethod = AuthMethod.Bearer;
        JWTPlugin = jwtPlugin;
        _httpHelper.AuthMethod = AuthMethod.Bearer;
    }

    /// <summary>
    /// Sets up the Auth Client to use Basic Authentication with Application Password
    /// </summary>
    /// <param name="username">Username for Basic Authentication</param>
    /// <param name="applicationPassword">Application Password for Basic Authentication</param>
    public void UseBasicAuth(string username, string applicationPassword)
    {
        ArgumentNullException.ThrowIfNull(username);
        ArgumentNullException.ThrowIfNull(applicationPassword);
        AuthMethod = AuthMethod.Basic;
        Username = username;
        _httpHelper.AuthMethod = AuthMethod.Basic;
        _httpHelper.UserName = username;
        _httpHelper.ApplicationPassword = applicationPassword;
    }

    /// <summary>
    /// Perform authentication by JWToken
    /// </summary>
    /// <param name="username">username</param>
    /// <param name="password">password</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task RequestJWTokenAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(username);
        ArgumentNullException.ThrowIfNull(password);
        if (AuthMethod == AuthMethod.Basic)
        {
            throw new WPException("Cannot request JWToken with Basic Authentication");
        }

        var route = $"{JwtPath}token";
        using var formContent = new FormUrlEncodedContent(new[]
        {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
        });

        switch (JWTPlugin)
        {
            case JWTPlugin.JWTAuthByEnriqueChavez:
                (JWTUser? jwtUser, HttpResponseMessage _) = await _httpHelper.PostRequestAsync<JWTUser>(route, formContent, isAuthRequired: false, ignoreDefaultPath: true, cancellationToken: cancellationToken).ConfigureAwait(false);
                _httpHelper.JWToken = jwtUser?.Token;
                break;
            case JWTPlugin.JWTAuthByUsefulTeam:
                _httpHelper.HttpResponsePreProcessing = RemoveEmptyData;
                try
                {
                    (JWTResponse? jwtResponse, HttpResponseMessage _) = await _httpHelper.PostRequestAsync<JWTResponse>(route, formContent, isAuthRequired: false, ignoreDefaultPath: true, cancellationToken: cancellationToken).ConfigureAwait(false);
                    _httpHelper.JWToken = jwtResponse?.Data?.Token;
                }
                finally
                {
                    _httpHelper.HttpResponsePreProcessing = null;
                }
                break;
            default:
                throw new WPException("Invalid JWT Plugin");
        }
    }

    /// <summary>
    /// Forget the JWT Auth Token, won't invalidate it serverside though
    /// </summary>
    public void Logout()
    {
        _httpHelper.JWToken = null;
        _httpHelper.UserName = null;
        _httpHelper.ApplicationPassword = null;
    }

    /// <summary>
    /// Check if token is valid
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of checking</returns>
    public async Task<bool> IsValidJWTokenAsync(CancellationToken cancellationToken = default)
    {
        if (AuthMethod == AuthMethod.Basic)
        {
            throw new WPException("Cannot check validity of JWToken with Basic Authentication");
        }

        var route = $"{JwtPath}token/validate";
        try
        {
            switch (JWTPlugin)
            {
                case JWTPlugin.JWTAuthByEnriqueChavez:
                    (JWTUser? jwtUser, HttpResponseMessage? response) = await _httpHelper.PostRequestAsync<JWTUser>(route, null, isAuthRequired: true, ignoreDefaultPath: true, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return response.IsSuccessStatusCode;
                case JWTPlugin.JWTAuthByUsefulTeam:
                    _httpHelper.HttpResponsePreProcessing = RemoveEmptyData;
                    try
                    {
                        (JWTResponse? jwtResponse, HttpResponseMessage _) = await _httpHelper.PostRequestAsync<JWTResponse>(route, null, isAuthRequired: true, ignoreDefaultPath: true, cancellationToken: cancellationToken).ConfigureAwait(false);
                        return jwtResponse.Success;
                    }
                    finally
                    {
                        _httpHelper.HttpResponsePreProcessing = null;
                    }
                default:
                    throw new WPException("Invalid JWT Plugin");
            }
        }
        catch (WPException)
        {
            return false;
        }
    }

    /// <summary>
    /// Removes an empty data field in a Json string, e.g. when checking validity of token
    /// </summary>
    /// <param name="response">Json response input string</param>
    /// <returns>Json response output string</returns>
    private string RemoveEmptyData(string response)
    {
        using JsonDocument doc = JsonDocument.Parse(response);
        JsonElement root = doc.RootElement;

        // If "data" property is missing, return as-is
        if (!root.TryGetProperty("data", out JsonElement dataElement))
        {
            return response;
        }

        // Match Newtonsoft JToken.HasValues semantics: data "has values" when it is a
        // non-empty object or a non-empty array. Primitives (string, number, bool, null)
        // and empty containers have no values and should be removed.
        bool dataHasValues = dataElement.ValueKind switch
        {
            JsonValueKind.Object => dataElement.EnumerateObject().MoveNext(),
            JsonValueKind.Array => dataElement.EnumerateArray().MoveNext(),
            _ => false
        };

        if (dataHasValues)
        {
            return response;
        }

        // Re-serialize without the "data" property
        using MemoryStream ms = new();
        using (Utf8JsonWriter writer = new(ms))
        {
            writer.WriteStartObject();
            foreach (JsonProperty prop in root.EnumerateObject())
            {
                if (prop.Name != "data")
                {
                    prop.WriteTo(writer);
                }
            }
            writer.WriteEndObject();
        }
        return Encoding.UTF8.GetString(ms.ToArray());
    }

    /// <summary>
    /// Sets an existing JWToken
    /// </summary>
    /// <param name="token"></param>
    public void SetJWToken(string token)
    {
        _httpHelper.JWToken = token;
    }

    /// <summary>
    /// Gets the JWToken from the client
    /// </summary>
    /// <returns></returns>
    public string? GetToken()
    {
        return _httpHelper.JWToken;
    }

}
