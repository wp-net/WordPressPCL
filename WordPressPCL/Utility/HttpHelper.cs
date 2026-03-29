using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Models.Exceptions;

namespace WordPressPCL.Utility;

/// <summary>
/// Helper class encapsulates common HTTP requests methods
/// </summary>
public class HttpHelper
{
    // non-static HTTPClient so different WordPressClients can have different base addresses
    private readonly HttpClient _defaultHttpClient = new();
    private readonly HttpClient _httpClient;
    private readonly string _defaultPath;
    private readonly Uri? _baseUri;

    /// <summary>
    /// JSON Web Token
    /// </summary>
    public string? JWToken { get; set; }

    /// <summary>
    /// The Application Password to be used for authentication
    /// </summary>
    internal string? ApplicationPassword { get; set; }

    /// <summary>
    /// Authentication Method
    /// </summary>
    internal AuthMethod AuthMethod { get; set; }

    /// <summary>
    /// The username to be used with the Application Password
    /// </summary>
    internal string? UserName { get; set; }

    /// <summary>
    /// Function called when a HttpRequest response is read
    /// Executed before trying to convert json content to a TClass object.
    /// </summary>
    public Func<string, string>? HttpResponsePreProcessing { get; set; }


    /// <summary>
    /// Serialization/deserialization options for <see cref="System.Text.Json.JsonSerializer"/>.
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions { get; set; }

    /// <summary>
    /// Constructor
    /// <paramref name="wordpressURI"/>
    /// </summary>
    /// <param name="wordpressURI">base WP REST API endpoint EX. http://demo.com/wp-json/ </param>
    /// <param name="defaultPath"></param>
    public HttpHelper(Uri wordpressURI, string defaultPath)
    {
        _httpClient = _defaultHttpClient;
        _httpClient.BaseAddress = wordpressURI;
        _defaultPath = defaultPath;
        JsonSerializerOptions = CreateDefaultOptions();
    }

    /// <summary>
    /// Constructor
    /// <paramref name="httpClient"/>
    /// </summary>
    /// <param name="httpClient">Http client which would be used for sending requests to the WordPress API endpoint.</param>
    /// <param name="defaultPath">Relative path to standard API endpoints, defaults to "wp/v2/"</param>
    /// <param name="wordpressURI">(optional) Base WP REST API endpoint EX. http://demo.com/wp-json/. Use this if the BaseAddress of the httpClient is not set.</param>
    public HttpHelper(HttpClient httpClient, string defaultPath, Uri? wordpressURI = null)
    {
        _httpClient = httpClient;
        _defaultPath = defaultPath;
        _baseUri = wordpressURI;
        JsonSerializerOptions = CreateDefaultOptions();
    }

    internal async Task<TClass> GetRequestAsync<TClass>(string route, bool embed, bool isAuthRequired = false, bool ignoreDefaultPath = false, CancellationToken cancellationToken = default)
        where TClass : class
    {
        (TClass? result, HttpResponseHeaders _) = await GetRequestWithHeadersAsync<TClass>(route, embed, isAuthRequired, ignoreDefaultPath, cancellationToken).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// Issues a GET request and returns both the deserialized response and the raw response headers.
    /// Use <see cref="ParsePaginationHeaders"/> to extract <c>X-WP-Total</c> / <c>X-WP-TotalPages</c> values.
    /// </summary>
    internal async Task<(TClass result, HttpResponseHeaders headers)> GetRequestWithHeadersAsync<TClass>(string route, bool embed, bool isAuthRequired = false, bool ignoreDefaultPath = false, CancellationToken cancellationToken = default)
        where TClass : class
    {
        route = BuildRoute(ignoreDefaultPath, route);
        string embedParam = "";
        if (embed)
        {
            embedParam = route.Contains('?') ? "&_embed" : "?_embed";
        }
        route += embedParam;

        HttpResponseMessage response;
        using (HttpRequestMessage requestMessage = new(HttpMethod.Get, route))
        {
            SetAuthHeader(isAuthRequired, requestMessage);
            response = await _httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
        }

        using (response)
        {
            string responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                if (HttpResponsePreProcessing != null)
                {
                    responseString = HttpResponsePreProcessing(responseString);
                }

                TClass result = DeserializeJsonResponse<TClass>(response, responseString);
                HttpResponseHeaders capturedHeaders = response.Headers;
                return (result, capturedHeaders);
            }
            else
            {
                throw CreateUnexpectedResponseException(response, responseString);
            }
        }
    }

    /// <summary>
    /// Parses the WordPress pagination headers from a response.
    /// </summary>
    /// <param name="headers">The response headers to inspect.</param>
    /// <returns>
    /// A tuple of (<c>total</c>, <c>totalPages</c>) where <c>total</c> comes from
    /// <c>X-WP-Total</c> and <c>totalPages</c> comes from <c>X-WP-TotalPages</c>.
    /// Returns <c>(0, 1)</c> when the headers are absent.
    /// </returns>
    internal static (int total, int totalPages) ParsePaginationHeaders(HttpResponseHeaders headers)
    {
        int total = 0;
        int totalPages = 1;

        if (headers.Contains("X-WP-Total") &&
            int.TryParse(headers.GetValues("X-WP-Total").FirstOrDefault(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int t))
        {
            total = t;
        }

        if (headers.Contains("X-WP-TotalPages") &&
            int.TryParse(headers.GetValues("X-WP-TotalPages").FirstOrDefault(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int tp))
        {
            totalPages = tp;
        }

        return (total, totalPages);
    }

    internal async Task<(TClass, HttpResponseMessage)> PostRequestAsync<TClass>(string route, HttpContent? postBody, bool isAuthRequired = true, bool ignoreDefaultPath = false, CancellationToken cancellationToken = default)
        where TClass : class
    {
        route = BuildRoute(ignoreDefaultPath, route);
        HttpResponseMessage response;
        using (HttpRequestMessage requestMessage = new(HttpMethod.Post, route))
        {
            SetAuthHeader(isAuthRequired, requestMessage);
            requestMessage.Content = postBody;
            response = await _httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
        }

        string responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            if (HttpResponsePreProcessing != null)
            {
                responseString = HttpResponsePreProcessing(responseString);
            }

            return (DeserializeJsonResponse<TClass>(response, responseString), response);
        }
        else
        {
            throw CreateUnexpectedResponseException(response, responseString);
        }
    }

    internal async Task<bool> DeleteRequestAsync(string route, bool isAuthRequired = true, bool ignoreDefaultPath = false, CancellationToken cancellationToken = default)
    {
        route = BuildRoute(ignoreDefaultPath, route);
        HttpResponseMessage response;
        using (HttpRequestMessage requestMessage = new(HttpMethod.Delete, route))
        {
            SetAuthHeader(isAuthRequired, requestMessage);
            response = await _httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
        }

        string responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw CreateUnexpectedResponseException(response, responseString);
        }
    }

    internal async Task<HttpResponseHeaders> HeadRequestAsync(string route, bool isAuthRequired = false, bool ignoreDefaultPath = false, CancellationToken cancellationToken = default)
    {
        route = BuildRoute(ignoreDefaultPath, route);
        HttpResponseMessage response;
        using (HttpRequestMessage requestMessage = new(HttpMethod.Head, route))
        {
            SetAuthHeader(isAuthRequired, requestMessage);
            response = await _httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
        }

        if (response.IsSuccessStatusCode)
        {
            return response.Headers;
        }

        throw new WPUnexpectedException(response, string.Empty);
    }

    private void SetAuthHeader(bool isAuthRequired, HttpRequestMessage requestMessage)
    {
        if (isAuthRequired)
        {
            if (AuthMethod == AuthMethod.Bearer)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWToken);
            }
            else if (AuthMethod == AuthMethod.Basic)
            {
                byte[] authToken = Encoding.ASCII.GetBytes($"{UserName}:{ApplicationPassword}");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            }
            else
            {
                throw new WPException("Unsupported Authentication Method");
            }
        }
    }

    private string BuildRoute(bool ignoreDefaultPath, string route)
    {
        if (string.IsNullOrEmpty(route))
        {
            return route;
        }

        string processedRoute = ignoreDefaultPath ? route : $"{_defaultPath}{route}";

        if (_baseUri != null)
        {
            processedRoute = $"{_baseUri.AbsoluteUri.TrimEnd('/')}/{processedRoute.TrimStart('/')}";
        }

        return processedRoute;
    }

    private TClass DeserializeJsonResponse<TClass>(HttpResponseMessage response, string responseString)
    {
        try
        {
            TClass? result = JsonSerializer.Deserialize<TClass>(responseString, JsonSerializerOptions);

            if (result is null)
            {
                throw new WPUnexpectedException(response, responseString);
            }

            return result;
        }
        catch (JsonException)
        {
            (bool success, string sanitizedResponse) = TryGetResponseFromMalformedResponse(responseString);
            if (!success)
            {
                throw new WPUnexpectedException(response, responseString);
            }

            TClass? result = JsonSerializer.Deserialize<TClass>(sanitizedResponse, JsonSerializerOptions);

            if (result is null)
            {
                throw new WPUnexpectedException(response, sanitizedResponse);
            }

            return result;
        }
    }

    private static readonly Regex s_jsonSingleItemRegex = new(@"\{""id"":.+\}$", RegexOptions.Compiled);
    private static readonly Regex s_jsonCollectionRegex = new(@"\[({""id"":.+},?)*\]$", RegexOptions.Compiled);

    private static (bool, string) TryGetResponseFromMalformedResponse(string responseString)
    {
        responseString = responseString.Trim();
        Match match = s_jsonSingleItemRegex.Match(responseString);
        if (match.Success)
        {
            return (true, match.Value);
        }
        match = s_jsonCollectionRegex.Match(responseString);
        if (match.Success)
        {
            return (true, match.Value);
        }

        return (false, string.Empty);
    }

    private static Exception CreateUnexpectedResponseException(HttpResponseMessage response, string responseString)
    {
        BadRequest? badrequest;
        try
        {
            badrequest = JsonSerializer.Deserialize<BadRequest>(responseString);
        }
        catch (JsonException)
        {
            // the response is not a well formed bad request
            return new WPUnexpectedException(response, responseString);
        }
        if (badrequest == null)
        {
            return new WPUnexpectedException(response, responseString);
        }
        return new WPException(badrequest.Message ?? "The server returned an error without providing a message.", badrequest);
    }

    private static JsonSerializerOptions CreateDefaultOptions()
    {
        return new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumMemberConverter() }
        };
    }
}
