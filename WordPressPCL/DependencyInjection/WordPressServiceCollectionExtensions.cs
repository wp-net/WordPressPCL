using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Dependency injection extensions for registering <see cref="WordPressPCL.WordPressClient"/> with <see cref="IHttpClientFactory"/>.
/// </summary>
public static class WordPressServiceCollectionExtensions
{
    private const string DefaultPath = "wp/v2/";
    private const string ClientName = "WordPressPCL.WordPressClient";

    /// <summary>
    /// Registers <see cref="WordPressPCL.WordPressClient"/> as a typed client with a fixed WordPress API base address.
    /// </summary>
    /// <param name="services">The DI service collection.</param>
    /// <param name="wordpressUri">The WordPress REST API base address, such as <c>https://example.com/wp-json/</c>.</param>
    /// <param name="configureWordPressClient">Optional callback for configuring WordPress-specific defaults such as authentication.</param>
    /// <param name="defaultPath">Relative path to standard API endpoints, defaults to <c>wp/v2/</c>.</param>
    /// <returns>The HTTP client builder for additional configuration such as custom handlers.</returns>
    public static IHttpClientBuilder AddWordPressClient(
        this IServiceCollection services,
        Uri wordpressUri,
        Action<IServiceProvider, WordPressPCL.WordPressClient>? configureWordPressClient = null,
        string defaultPath = DefaultPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(wordpressUri);

        return services.AddWordPressClient(
            (_, httpClient) => httpClient.BaseAddress = wordpressUri,
            configureWordPressClient,
            defaultPath);
    }

    /// <summary>
    /// Registers <see cref="WordPressPCL.WordPressClient"/> as a typed client and lets the caller configure the underlying <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="services">The DI service collection.</param>
    /// <param name="configureHttpClient">Callback used to configure the underlying <see cref="HttpClient"/>, including its base address.</param>
    /// <param name="configureWordPressClient">Optional callback for configuring WordPress-specific defaults such as authentication.</param>
    /// <param name="defaultPath">Relative path to standard API endpoints, defaults to <c>wp/v2/</c>.</param>
    /// <returns>The HTTP client builder for additional configuration such as custom handlers.</returns>
    public static IHttpClientBuilder AddWordPressClient(
        this IServiceCollection services,
        Action<IServiceProvider, HttpClient> configureHttpClient,
        Action<IServiceProvider, WordPressPCL.WordPressClient>? configureWordPressClient = null,
        string defaultPath = DefaultPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureHttpClient);

        return services
            .AddHttpClient(ClientName, configureHttpClient)
            .AddTypedClient((httpClient, serviceProvider) =>
            {
                WordPressPCL.WordPressClient client = new(httpClient, defaultPath);
                configureWordPressClient?.Invoke(serviceProvider, client);
                return client;
            });
    }

    /// <summary>
    /// Registers <see cref="WordPressPCL.WordPressClient"/> as a typed client with simple callbacks that do not require the <see cref="IServiceProvider"/>.
    /// </summary>
    /// <param name="services">The DI service collection.</param>
    /// <param name="configureHttpClient">Callback used to configure the underlying <see cref="HttpClient"/>, including its base address.</param>
    /// <param name="configureWordPressClient">Optional callback for configuring WordPress-specific defaults such as authentication.</param>
    /// <param name="defaultPath">Relative path to standard API endpoints, defaults to <c>wp/v2/</c>.</param>
    /// <returns>The HTTP client builder for additional configuration such as custom handlers.</returns>
    public static IHttpClientBuilder AddWordPressClient(
        this IServiceCollection services,
        Action<HttpClient> configureHttpClient,
        Action<WordPressPCL.WordPressClient>? configureWordPressClient = null,
        string defaultPath = DefaultPath)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureHttpClient);

        return services.AddWordPressClient(
            (_, httpClient) => configureHttpClient(httpClient),
            configureWordPressClient == null ? null : (_, client) => configureWordPressClient(client),
            defaultPath);
    }
}
