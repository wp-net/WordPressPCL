using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client;

/// <summary>
/// Client class for interaction with the Navigation endpoint WP REST API (<c>wp/v2/navigation</c>).
/// </summary>
public class NavigationClient : CRUDOperation<Navigation, NavigationQueryBuilder>
{
    private const string _methodPath = "navigation";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpHelper">reference to HttpHelper class for interaction with HTTP</param>
    public NavigationClient(HttpHelper httpHelper) : base(httpHelper, _methodPath)
    {
    }
}
