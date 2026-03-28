using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with Comments endpoint WP REST API
/// </summary>

public class Comments : CRUDOperation<Comment, CommentsQueryBuilder>
{
    #region Init

    private const string _methodPath = "comments";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Comments(HttpHelper HttpHelper) : base(HttpHelper, _methodPath)
    {
    }

    #endregion Init

    #region Custom

    /// <summary>
    /// Get comments for Post
    /// </summary>
    /// <param name="PostID">Post id</param>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of comments for post</returns>
    public Task<List<Comment>> GetCommentsForPostAsync(int PostID, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        return HttpHelper.GetRequestAsync<List<Comment>>($"{_methodPath}?post={PostID}", embed, useAuth, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get all comments for Post
    /// </summary>
    /// <param name="PostID">Post id</param>
    /// <param name="embed">include embed info</param>
    /// <param name="useAuth">Send request with authentication header</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of comments for post</returns>
    public async Task<List<Comment>> GetAllCommentsForPostAsync(int PostID, bool embed = false, bool useAuth = false, CancellationToken cancellationToken = default)
    {
        //100 - Max comments per page in WordPress REST API, so this is hack with multiple requests
        (List<Comment> comments, System.Net.Http.Headers.HttpResponseHeaders headers) = await HttpHelper.GetRequestWithHeadersAsync<List<Comment>>($"{_methodPath}?post={PostID}&per_page=100&page=1", embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false);
        (int _, int totalPages) = HttpHelper.ParsePaginationHeaders(headers);
        for (int page = 2; page <= totalPages; page++)
        {
            comments.AddRange(await HttpHelper.GetRequestAsync<List<Comment>>($"{_methodPath}?post={PostID}&per_page=100&page={page}", embed, useAuth, cancellationToken: cancellationToken).ConfigureAwait(false));
        }
        return comments;
    }

    /// <summary>
    /// Force deletion of comments
    /// </summary>
    /// <param name="ID">Comment Id</param>
    /// <param name="force">force deletion</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result of operation</returns>
    public Task<bool> DeleteAsync(int ID, bool force = false, CancellationToken cancellationToken = default)
    {
        return HttpHelper.DeleteRequestAsync($"{_methodPath}/{ID}?force={force.ToString().ToLower(CultureInfo.InvariantCulture)}", cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Update Comment
    /// </summary>
    /// <param name="Entity">Comment object</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated comment</returns>
    public new async Task<Comment> UpdateAsync(Comment Entity, CancellationToken cancellationToken = default)
    {
        Dictionary<string, object> body = new();

        if (Entity.PostId != 0)
        {
            body["post"] = Entity.PostId;
        }

        if (Entity.ParentId != 0)
        {
            body["parent"] = Entity.ParentId;
        }

        if (Entity.AuthorId != 0)
        {
            body["author"] = Entity.AuthorId;
        }

        if (!string.IsNullOrWhiteSpace(Entity.AuthorName))
        {
            body["author_name"] = Entity.AuthorName;
        }

        if (!string.IsNullOrWhiteSpace(Entity.AuthorEmail))
        {
            body["author_email"] = Entity.AuthorEmail;
        }

        if (!string.IsNullOrWhiteSpace(Entity.AuthorUrl))
        {
            body["author_url"] = Entity.AuthorUrl;
        }

        body["status"] = GetEnumMemberValue(Entity.Status);

        if (Entity.Karma != 0)
        {
            body["karma"] = Entity.Karma;
        }

        if (HasNonNullValue(Entity.Meta))
        {
            body["meta"] = Entity.Meta!;
        }
        if (!string.IsNullOrWhiteSpace(Entity.Content?.Raw) || !string.IsNullOrWhiteSpace(Entity.Content?.Rendered))
        {
            body["content"] = new
            {
                raw = Entity.Content?.Raw ?? Entity.Content?.Rendered
            };
        }

        string entity = JsonSerializer.Serialize(body, HttpHelper.JsonSerializerOptions);
        using StringContent postBody = new(entity, Encoding.UTF8, "application/json");
        return (await HttpHelper.PostRequestAsync<Comment>($"{_methodPath}/{Entity?.Id}", postBody, cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    private static string GetEnumMemberValue<TEnum>(TEnum value) where TEnum : struct, System.Enum
    {
        EnumMemberAttribute? attribute = typeof(TEnum)
            .GetRuntimeField(value.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>();

        return attribute?.Value ?? value.ToString().ToLowerInvariant();
    }

    private static bool HasNonNullValue(object? value)
    {
        if (value is null)
        {
            return false;
        }

        JsonElement element;
        try
        {
            element = JsonSerializer.SerializeToElement(value);
        }
        catch
        {
            // If the value cannot be serialized, fall back to considering it as having a value
            // to avoid silently dropping data compared to the previous behavior.
            return true;
        }

        return HasNonNullValue(element);
    }

    private static bool HasNonNullValue(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Null:
            case JsonValueKind.Undefined:
                return false;

            case JsonValueKind.String:
            case JsonValueKind.Number:
            case JsonValueKind.True:
            case JsonValueKind.False:
                return true;

            case JsonValueKind.Array:
                foreach (JsonElement item in element.EnumerateArray())
                {
                    if (HasNonNullValue(item))
                    {
                        return true;
                    }
                }
                return false;

            case JsonValueKind.Object:
                foreach (System.Text.Json.JsonProperty property in element.EnumerateObject())
                {
                    if (HasNonNullValue(property.Value))
                    {
                        return true;
                    }
                }
                return false;

            default:
                return false;
        }
    }

    #endregion Custom
}
