using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordPressPCL.Tests.Hosted;

[TestClass]
public class WordPressClient_Logger_Tests
{
    [TestMethod]
    public async Task Logger_IsNull_ByDefault_NoExceptionThrown()
    {
        // When no logger is configured, requests succeed and no exception is thrown
        StubHandler handler = new(HttpStatusCode.OK, "[]");
        HttpClient httpClient = new(handler) { BaseAddress = new Uri("https://example.com/wp-json/") };
        using WordPressClient client = new(httpClient);

        Assert.IsNull(client.Logger);

        // Should not throw even without a logger
        List<Models.Post> posts = await client.Posts.GetAllAsync();
        Assert.AreEqual(0, posts.Count);
    }

    [TestMethod]
    public async Task Logger_Emits_DebugLog_OnSuccessfulGetRequest()
    {
        CapturingLogger logger = new();
        StubHandler handler = new(HttpStatusCode.OK, "[]");
        HttpClient httpClient = new(handler) { BaseAddress = new Uri("https://example.com/wp-json/") };
        using WordPressClient client = new(httpClient) { Logger = logger };

        await client.Posts.GetAllAsync();

        Assert.IsTrue(logger.HasLevel(LogLevel.Debug), "Expected at least one Debug log entry for a successful GET request.");
    }

    [TestMethod]
    public async Task Logger_Emits_WarningLog_OnNonSuccessGetRequest()
    {
        CapturingLogger logger = new();
        StubHandler handler = new(HttpStatusCode.NotFound, "{\"code\":\"rest_no_route\",\"message\":\"No route was found matching the URL and request method.\",\"data\":{\"status\":404}}");
        HttpClient httpClient = new(handler) { BaseAddress = new Uri("https://example.com/wp-json/") };
        using WordPressClient client = new(httpClient) { Logger = logger };

        bool exceptionThrown = false;
        try
        {
            _ = await client.Posts.GetByIdAsync(999);
        }
        catch (Models.Exceptions.WPException)
        {
            exceptionThrown = true;
        }
        catch (Models.Exceptions.WPUnexpectedException)
        {
            exceptionThrown = true;
        }

        Assert.IsTrue(exceptionThrown, "Expected a WPException or WPUnexpectedException for a non-success GET response.");
        Assert.IsTrue(logger.HasLevel(LogLevel.Warning), "Expected at least one Warning log entry for a non-success GET response.");
    }

    [TestMethod]
    public async Task Logger_Emits_DebugLog_OnSuccessfulPostRequest()
    {
        CapturingLogger logger = new();
        string postJson = "{\"id\":1,\"slug\":\"test\",\"status\":\"publish\",\"type\":\"post\",\"link\":\"https://example.com\",\"title\":{\"rendered\":\"Test\"},\"content\":{\"rendered\":\"Body\",\"protected\":false},\"excerpt\":{\"rendered\":\"\",\"protected\":false},\"author\":1,\"featured_media\":0,\"comment_status\":\"open\",\"ping_status\":\"open\",\"sticky\":false,\"template\":\"\",\"format\":\"standard\",\"categories\":[],\"tags\":[]}";
        StubHandler handler = new(HttpStatusCode.Created, postJson);
        HttpClient httpClient = new(handler) { BaseAddress = new Uri("https://example.com/wp-json/") };
        using WordPressClient client = new(httpClient) { Logger = logger };
        client.Auth.UseBasicAuth("user", "pass");

        await client.Posts.CreateAsync(new Models.Post { Title = new Models.Title { Raw = "Test" } });

        Assert.IsTrue(logger.HasLevel(LogLevel.Debug), "Expected at least one Debug log entry for a successful POST request.");
    }

    [TestMethod]
    public async Task Logger_Emits_WarningLog_OnNonSuccessPostRequest()
    {
        CapturingLogger logger = new();
        StubHandler handler = new(HttpStatusCode.Forbidden, "{\"code\":\"rest_cannot_create\",\"message\":\"Sorry, you are not allowed to create posts.\",\"data\":{\"status\":403}}");
        HttpClient httpClient = new(handler) { BaseAddress = new Uri("https://example.com/wp-json/") };
        using WordPressClient client = new(httpClient) { Logger = logger };
        client.Auth.UseBasicAuth("user", "pass");

        bool exceptionThrown = false;
        try
        {
            await client.Posts.CreateAsync(new Models.Post { Title = new Models.Title { Raw = "Test" } });
            Assert.Fail("Expected WPException or WPUnexpectedException to be thrown for a non-success POST response.");
        }
        catch (Models.Exceptions.WPException)
        {
            exceptionThrown = true;
        }
        catch (Models.Exceptions.WPUnexpectedException)
        {
            exceptionThrown = true;
        }

        Assert.IsTrue(exceptionThrown, "Expected a WPException or WPUnexpectedException for a non-success POST response.");
        Assert.IsTrue(logger.HasLevel(LogLevel.Warning), "Expected at least one Warning log entry for a non-success POST response.");
    }

    [TestMethod]
    public async Task AddWordPressClient_WiresLogger_FromDIContainer()
    {
        CapturingLogger logger = new();
        CapturingLoggerFactory loggerFactory = new(logger);
        StubHandler handler = new(HttpStatusCode.OK, "[]");

        ServiceCollection services = new();
        services.AddSingleton<ILoggerFactory>(loggerFactory);
        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

        services
            .AddWordPressClient(
                (_, httpClient) => httpClient.BaseAddress = new Uri("https://example.com/wp-json/"))
            .ConfigurePrimaryHttpMessageHandler(() => handler);

        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        using WordPressClient client = serviceProvider.GetRequiredService<WordPressClient>();

        Assert.IsNotNull(client.Logger, "Logger should be wired from DI container automatically.");

        await client.Posts.GetAllAsync();

        Assert.IsTrue(logger.HasLevel(LogLevel.Debug), "Expected Debug log entries from the DI-wired logger.");
    }

    // ── helpers ──────────────────────────────────────────────────────────────

    private sealed class StubHandler(HttpStatusCode statusCode, string body) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            });
    }

    /// <summary>A minimal <see cref="ILogger"/> that records every log entry for assertion.</summary>
    private sealed class CapturingLogger : ILogger
    {
        private readonly List<(LogLevel Level, string Message)> _entries = [];

        public bool HasLevel(LogLevel level) => _entries.Exists(e => e.Level == level);

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            => _entries.Add((logLevel, formatter(state, exception)));
    }

    /// <summary>A minimal <see cref="ILoggerFactory"/> that always returns the same <see cref="CapturingLogger"/>.</summary>
    private sealed class CapturingLoggerFactory(CapturingLogger logger) : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider) { }
        public ILogger CreateLogger(string categoryName) => logger;
        public void Dispose() { }
    }
}
