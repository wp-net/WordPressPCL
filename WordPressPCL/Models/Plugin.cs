using System.Text.Json.Serialization;


namespace WordPressPCL.Models
{
    /// <summary>
    /// WordPress Plugins
    /// Date: 26 May 2023
    /// Creator: Gregory Liénard
    /// </summary>
    public class Plugin
    {
        /// <summary>
        /// The plugin file. unique identifier.
        /// </summary>
        [JsonPropertyName("plugin")]
        public string? PluginFile
        {
            get
            {
                return _PluginFile;
            }
            set
            {
                _PluginFile = value;
                if (value != null && value.Contains("/"))
                    Id = value.Substring(0, value.IndexOf("/"));
                else
                    Id = value;
            }
        }
        private string? _PluginFile;

        /// <summary>
        /// First part of the pluginfile: wordpress-seo/wp-seo => wordpress-seo
        /// Id is needed to install, PluginFile is needed to activate, deactivate, delete
        /// </summary>
        public string? Id { get ; set; }

        /// <summary>
        /// The plugin activation status.
        /// </summary>
        [JsonPropertyName("status")]
        public ActivationStatus Status { get; set; }

        /// <summary>
        /// The plugin name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// The plugin's website address.
        /// </summary>
        [JsonPropertyName("plugin_uri")]
        public string? PluginUri { get; set; }

        /// <summary>
        /// The plugin author.
        /// </summary>
        [JsonPropertyName("author")]
        public string? Author { get; set; }

        /// <summary>
        /// Plugin author's website address.
        /// </summary>
        [JsonPropertyName("author_uri")]
        public string? AuthorUri { get; set; }

        /// <summary>
        /// The plugin description.
        /// </summary>
        [JsonPropertyName("description")]
        public Description? Description { get; set; }

        /// <summary>
        /// The plugin version number.
        /// </summary>
        [JsonPropertyName("version")]
        public string? Version { get; set; }

        /// <summary>
        /// Whether the plugin can only be activated network-wide.
        /// </summary>
        [JsonPropertyName("network_only")]
        public bool NetworkOnly { get; set; }

        /// <summary>
        /// Minimum required version of WordPress.
        /// </summary>
        [JsonPropertyName("requires_wp")]
        public string? RequiresWordPress { get; set; }

        /// <summary>
        /// Minimum required version of PHP.
        /// </summary>
        [JsonPropertyName("requires_php")]
        public string? RequiresPHP { get; set; }

        /// <summary>
        /// The plugin's text domain.
        /// </summary>
        [JsonPropertyName("textdomain")]
        public string? TextDomain { get; set; }

        /// <summary>
        /// The plugin's links.
        /// </summary>
        [JsonPropertyName("_links")]
        public Links? Links { get; set; }
    }
}