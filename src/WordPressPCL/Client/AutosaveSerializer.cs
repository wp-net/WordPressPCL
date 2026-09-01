using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using WordPressPCL.Models;

namespace WordPressPCL.Client;

internal static class AutosaveSerializer
{
    public static string Serialize(Post post, JsonSerializerOptions options)
    {
        return Serialize(
            post.Title?.Raw,
            post.Content?.Raw,
            post.Excerpt?.Raw,
            post.Meta,
            post.CustomFields,
            options);
    }

    public static string Serialize(Page page, JsonSerializerOptions options)
    {
        return Serialize(
            page.Title?.Raw,
            page.Content?.Raw,
            page.Excerpt?.Raw,
            page.Meta,
            page.CustomFields,
            options);
    }

    private static string Serialize(
        string? title,
        string? content,
        string? excerpt,
        JsonElement? meta,
        IDictionary<string, object>? customFields,
        JsonSerializerOptions options)
    {
        AutosaveRequest request = new()
        {
            Title = title,
            Content = content,
            Excerpt = excerpt,
            Meta = meta,
            CustomFields = FilterReservedFields(customFields)
        };

        return JsonSerializer.Serialize(request, options);
    }

    private static IDictionary<string, object>? FilterReservedFields(IDictionary<string, object>? customFields)
    {
        if (customFields is null)
        {
            return null;
        }

        Dictionary<string, object> filteredFields = [];
        foreach ((string key, object value) in customFields)
        {
            if (key is not ("title" or "content" or "excerpt" or "meta"))
            {
                filteredFields.Add(key, value);
            }
        }

        return filteredFields;
    }

    private sealed class AutosaveRequest
    {
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Content { get; set; }

        [JsonPropertyName("excerpt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Excerpt { get; set; }

        [JsonPropertyName("meta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? Meta { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object>? CustomFields { get; set; }
    }
}
