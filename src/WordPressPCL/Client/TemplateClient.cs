using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

internal sealed class TemplateClient<TTemplate, TQuery>
    where TTemplate : class
    where TQuery : QueryBuilder
{
    private readonly HttpHelper _httpHelper;
    private readonly string _methodPath;

    public TemplateClient(HttpHelper httpHelper, string methodPath)
    {
        _httpHelper = httpHelper;
        _methodPath = methodPath;
    }

    public Task<List<TTemplate>> GetAsync(
        bool embed,
        Context context,
        bool useAuth,
        CancellationToken cancellationToken)
    {
        string route = AddContext(_methodPath, context);
        return _httpHelper.GetRequestAsync<List<TTemplate>>(
            route,
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    public Task<TTemplate> GetByIdAsync(
        string id,
        bool embed,
        Context context,
        bool useAuth,
        CancellationToken cancellationToken)
    {
        string route = AddContext($"{_methodPath}/{RestPath.EncodeSegments(id)}", context);
        return _httpHelper.GetRequestAsync<TTemplate>(
            route,
            embed,
            useAuth,
            cancellationToken: cancellationToken);
    }

    public Task<List<TTemplate>> QueryAsync(
        TQuery queryBuilder,
        bool useAuth,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(queryBuilder);
        return _httpHelper.GetRequestAsync<List<TTemplate>>(
            $"{_methodPath}{queryBuilder.BuildQuery()}",
            false,
            useAuth,
            cancellationToken: cancellationToken);
    }

    public async Task<TTemplate> CreateAsync(TTemplate entity, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);
        string json = SerializeWritableEntity(entity);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<TTemplate>(
            _methodPath,
            postBody,
            cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    public async Task<TTemplate> UpdateAsync(
        string id,
        TTemplate entity,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(entity);
        string json = SerializeWritableEntity(entity);
        using StringContent postBody = new(json, Encoding.UTF8, "application/json");
        return (await _httpHelper.PostRequestAsync<TTemplate>(
            $"{_methodPath}/{RestPath.EncodeSegments(id)}",
            postBody,
            cancellationToken: cancellationToken).ConfigureAwait(false)).Item1;
    }

    public Task<bool> DeleteAsync(string id, bool force, CancellationToken cancellationToken)
    {
        string path = $"{_methodPath}/{RestPath.EncodeSegments(id)}"
            .SetQueryParam(nameof(force), force.ToString().ToLowerInvariant());
        return _httpHelper.DeleteRequestAsync(path, cancellationToken: cancellationToken);
    }

    private static string AddContext(string route, Context context)
    {
        return context == Context.View
            ? route
            : route.SetQueryParam(nameof(context), context.ToString().ToLowerInvariant());
    }

    private string SerializeWritableEntity(TTemplate entity)
    {
        JsonObject payload = JsonSerializer.SerializeToNode(
            entity,
            _httpHelper.JsonSerializerOptions)!.AsObject();
        RemoveResponseOnlyFields(payload);
        FlattenWritableText(payload, "content");
        FlattenWritableText(payload, "title");
        return payload.ToJsonString(_httpHelper.JsonSerializerOptions);
    }

    private static void RemoveResponseOnlyFields(JsonObject payload)
    {
        string[] responseOnlyFields =
        [
            "id",
            "type",
            "source",
            "origin",
            "wp_id",
            "has_theme_file",
            "modified",
            "author_text",
            "original_source",
            "date",
            "is_custom",
            "plugin",
            "_links",
            "_embedded"
        ];

        foreach (string field in responseOnlyFields)
        {
            payload.Remove(field);
        }
    }

    private static void FlattenWritableText(JsonObject payload, string propertyName)
    {
        if (payload[propertyName] is JsonObject value && value["raw"] is JsonNode raw)
        {
            payload[propertyName] = raw.DeepClone();
        }
    }
}
