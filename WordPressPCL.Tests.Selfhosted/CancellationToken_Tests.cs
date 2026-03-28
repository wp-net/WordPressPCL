using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted;

/// <summary>
/// Unit tests that verify CancellationToken is correctly forwarded through
/// the HTTP stack without requiring a live WordPress instance.
/// </summary>
[TestClass]
public class CancellationToken_Tests
{
    /// <summary>
    /// Verifies that a pre-cancelled token causes the request to throw
    /// <see cref="TaskCanceledException"/> immediately.
    /// </summary>
    [TestMethod]
    public async Task GetAsync_WithPreCancelledToken_ThrowsTaskCanceledException()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        // Use a handler that records whether cancellation was observed
        using var handler = new CancellationCapturingHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("http://localhost:8080/wp-json/") };
        var client = new WordPressClient(httpClient);

        await Assert.ThrowsExactlyAsync<TaskCanceledException>(
            () => client.Posts.GetAsync(cancellationToken: cts.Token));
    }

    /// <summary>
    /// Verifies that cancellation during an in-flight request causes
    /// <see cref="TaskCanceledException"/> and that the token was passed to SendAsync.
    /// </summary>
    [TestMethod]
    public async Task GetAsync_CancelledDuringRequest_ThrowsTaskCanceledException()
    {
        using var cts = new CancellationTokenSource();
        using var handler = new DelayingCancellationHandler(cts);
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("http://localhost:8080/wp-json/") };
        var client = new WordPressClient(httpClient);

        await Assert.ThrowsExactlyAsync<TaskCanceledException>(
            () => client.Posts.GetAsync(cancellationToken: cts.Token));
    }

    /// <summary>
    /// Verifies that CancellationToken is threaded through PostRequestAsync (CreateAsync).
    /// </summary>
    [TestMethod]
    public async Task CreateAsync_WithPreCancelledToken_ThrowsTaskCanceledException()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        using var handler = new CancellationCapturingHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("http://localhost:8080/wp-json/") };
        var client = new WordPressClient(httpClient);

        await Assert.ThrowsExactlyAsync<TaskCanceledException>(
            () => client.Posts.CreateAsync(new Post { Title = new Title { Raw = "Test" } }, cts.Token));
    }

    /// <summary>
    /// Verifies that CancellationToken is threaded through DeleteRequestAsync.
    /// </summary>
    [TestMethod]
    public async Task DeleteAsync_WithPreCancelledToken_ThrowsTaskCanceledException()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        using var handler = new CancellationCapturingHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("http://localhost:8080/wp-json/") };
        var client = new WordPressClient(httpClient);

        await Assert.ThrowsExactlyAsync<TaskCanceledException>(
            () => client.Posts.DeleteAsync(1, cancellationToken: cts.Token));
    }

    /// <summary>
    /// Handler that immediately throws when a cancelled token is detected,
    /// simulating HttpClient behaviour with a pre-cancelled token.
    /// </summary>
    private sealed class CancellationCapturingHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // Should never reach here in these tests
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("[]") });
        }
    }

    /// <summary>
    /// Handler that signals cancellation when a request arrives, simulating
    /// a cancel that occurs while the request is in-flight.
    /// </summary>
    private sealed class DelayingCancellationHandler : HttpMessageHandler
    {
        private readonly CancellationTokenSource _cts;

        public DelayingCancellationHandler(CancellationTokenSource cts)
        {
            _cts = cts;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Cancel the source as soon as the request arrives
            _cts.Cancel();
            // Yield so the cancellation can propagate
            await Task.Yield();
            cancellationToken.ThrowIfCancellationRequested();
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("[]") };
        }
    }
}
