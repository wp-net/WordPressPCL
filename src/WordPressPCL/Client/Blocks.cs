using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the Reusable Blocks endpoint WP REST API (<c>wp/v2/blocks</c>).
/// </summary>
public class Blocks : CRUDOperation<Block, BlocksQueryBuilder>
{
    private const string _methodPath = "blocks";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public Blocks(HttpHelper httpHelper) : base(httpHelper, _methodPath)
    {
    }
}
