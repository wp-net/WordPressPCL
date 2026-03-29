using System;
using System.Net.Http;
using System.Text.Json;
using WordPressPCL.Client;
using WordPressPCL.Utility;

namespace WordPressPCL;

/// <summary>
/// Main class containing the wrapper client with all public API endpoints.
/// </summary>
public class WordPressClient : IDisposable
{
    private readonly HttpHelper _httpHelper;
    private const string DEFAULT_PATH = "wp/v2/";

    /// <summary>
    /// WordPressUri holds the WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/wp/v2/"
    /// </summary>
		public Uri WordPressUri { get; private set; }

    /// <summary>
    /// Function called when a HttpRequest response to WordPress APIs are read
    /// Executed before trying to convert json content to a TClass object.
    /// </summary>
    public Func<string, string>? HttpResponsePreProcessing
    {
        get => _httpHelper.HttpResponsePreProcessing;
        set => _httpHelper.HttpResponsePreProcessing = value;
    }

    /// <summary>
    /// Serialization/deserialization options for <see cref="JsonSerializer"/>.
    /// </summary>
    public JsonSerializerOptions JsonSerializerOptions
    {
        get => _httpHelper.JsonSerializerOptions;
        set => _httpHelper.JsonSerializerOptions = value;
    }

    /// <summary>
    /// Gets a value indicating whether this instance owns the underlying <see cref="HttpClient"/>
    /// and will dispose it when <see cref="Dispose"/> is called.
    /// </summary>
    public bool OwnsHttpClient { get; }

    /// <summary>
    /// Auth client interaction object
    /// </summary>
    public Auth Auth { get; private set; } = null!;

    //public string JWToken;
    /// <summary>
    /// Posts client interaction object
    /// </summary>
    public Posts Posts { get; private set; } = null!;

    /// <summary>
    /// Comments client interaction object
    /// </summary>
    public Comments Comments { get; private set; } = null!;

    /// <summary>
    /// Tags client interaction object
    /// </summary>
    public Tags Tags { get; private set; } = null!;

    /// <summary>
    /// Users client interaction object
    /// </summary>
    public Users Users { get; private set; } = null!;

    /// <summary>
    /// Media client interaction object
    /// </summary>
    public Media Media { get; private set; } = null!;

    /// <summary>
    /// Categories client interaction object
    /// </summary>
    public Categories Categories { get; private set; } = null!;

    /// <summary>
    /// Pages client interaction object
    /// </summary>
    public Pages Pages { get; private set; } = null!;

    /// <summary>
    /// Taxonomies client interaction object
    /// </summary>
    public Taxonomies Taxonomies { get; private set; } = null!;

    /// <summary>
    /// Post Types client interaction object
    /// </summary>
    public PostTypes PostTypes { get; private set; } = null!;

    /// <summary>
    /// Post Statuses client interaction object
    /// </summary>
    public PostStatuses PostStatuses { get; private set; } = null!;

    /// <summary>
    /// Custom Request client interaction object
    /// </summary>
    public CustomRequest CustomRequest { get; private set; } = null!;

    /// <summary>
    /// Settings client interaction object
    /// </summary>
    public Settings Settings { get; private set; } = null!;

    /// <summary>
    /// Plugins client interaction object
    /// </summary>
    public Plugins Plugins { get; private set; } = null!;

    /// <summary>
    /// Themes client interaction object
    /// </summary>
    public Themes Themes { get; private set; } = null!;

    /// <summary>
    /// The WordPressClient holds all connection infos and provides methods to call WordPress APIs.
    /// </summary>
    /// <param name="uri">URI for WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/"</param>
    /// <param name="defaultPath">Relative path to standard API endpoints, defaults to "wp/v2/"</param>
    public WordPressClient(Uri uri, string defaultPath = DEFAULT_PATH)
    {
        WordPressUri = uri ?? throw new ArgumentNullException(nameof(uri));
        _httpHelper = new HttpHelper(WordPressUri, defaultPath);
        OwnsHttpClient = true;
        SetupSubClients(_httpHelper);
    }

    /// <summary>
    /// The WordPressClient holds all connection infos and provides methods to call WordPress APIs.
    /// </summary>
    /// <param name="uri">URI for WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/"</param>
    /// <param name="defaultPath">Relative path to standard API endpoints, defaults to "wp/v2/"</param>
    public WordPressClient(
        string uri,
        string defaultPath = DEFAULT_PATH) : this(new Uri(uri), defaultPath)
    {
    }


    /// <summary>
    /// The WordPressClient holds all connection infos and provides methods to call WordPress APIs.
    /// </summary>
    /// <param name="httpClient">HttpClient with BaseAddress set which will be used for sending requests to the WordPress API endpoint. The caller retains ownership of this instance.</param>
    /// <param name="defaultPath">Relative path to standard API endpoints, defaults to "wp/v2/"</param>
    /// <param name="uri">URI for WordPress API endpoint, e.g. "http://demo.wp-api.org/wp-json/".  Use this if the BaseAddress of the httpClient is not set.</param>
    public WordPressClient(HttpClient httpClient, string defaultPath = DEFAULT_PATH, Uri? uri = null)
    {
        if (httpClient == null)
        {
            throw new ArgumentNullException(nameof(httpClient));
        }
        WordPressUri = uri ?? httpClient.BaseAddress ?? throw new ArgumentNullException(nameof(uri), "Either uri or httpClient.BaseAddress must be set.");
        _httpHelper = new HttpHelper(httpClient, defaultPath, uri);
        OwnsHttpClient = false;
        SetupSubClients(_httpHelper);
    }

    /// <summary>
    /// Disposes the internally created <see cref="HttpClient"/> when this instance owns it.
    /// Externally supplied <see cref="HttpClient"/> instances are never disposed by <see cref="WordPressClient"/>.
    /// </summary>
    public void Dispose()
    {
        _httpHelper.Dispose();
    }

    private void SetupSubClients(HttpHelper httpHelper)
    {
        Auth = new Auth(httpHelper);
        Posts = new Posts(httpHelper);
        Comments = new Comments(httpHelper);
        Tags = new Tags(httpHelper);
        Users = new Users(httpHelper);
        Media = new Media(httpHelper);
        Categories = new Categories(httpHelper);
        Pages = new Pages(httpHelper);
        Taxonomies = new Taxonomies(httpHelper);
        PostTypes = new PostTypes(httpHelper);
        PostStatuses = new PostStatuses(httpHelper);
        CustomRequest = new CustomRequest(httpHelper);
        Settings = new Settings(httpHelper);
        Plugins = new Plugins(httpHelper);
        Themes = new Themes(httpHelper);
    }

}
