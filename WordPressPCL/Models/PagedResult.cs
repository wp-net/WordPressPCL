using System.Collections.Generic;

namespace WordPressPCL.Models;

/// <summary>
/// Represents a single page of results from a WordPress REST API list endpoint,
/// carrying both the items and the pagination metadata extracted from the
/// <c>X-WP-Total</c> and <c>X-WP-TotalPages</c> response headers.
/// </summary>
/// <typeparam name="T">The item type returned by the endpoint.</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// The items returned in this page.
    /// </summary>
    public IReadOnlyList<T> Items { get; }

    /// <summary>
    /// Total number of items across all pages, as reported by the
    /// <c>X-WP-Total</c> response header.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Total number of pages available, as reported by the
    /// <c>X-WP-TotalPages</c> response header.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="PagedResult{T}"/>.
    /// </summary>
    /// <param name="items">Items in this page.</param>
    /// <param name="totalCount">Total item count from <c>X-WP-Total</c>.</param>
    /// <param name="totalPages">Total page count from <c>X-WP-TotalPages</c>.</param>
    public PagedResult(IReadOnlyList<T> items, int totalCount, int totalPages)
    {
        Items = items;
        TotalCount = totalCount;
        TotalPages = totalPages;
    }
}
