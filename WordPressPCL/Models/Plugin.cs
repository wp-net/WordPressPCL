using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    /// <summary>
    /// WordPress main settings
    /// </summary>
    public class Plugin
    {
        /// <summary>
        /// The plugin file. unique identifier.
        /// </summary>
        [JsonProperty("plugin")]
        public string PluginFile
        {
            get
            {
                return _PluginFile;
            }
            set
            {
                _PluginFile = value;
                if (value.Contains("/"))
                    Id = value.Substring(0, value.IndexOf("/"));
                else
                    Id = value;
            }
        }
        private string _PluginFile;

        /// <summary>
        /// First part of the pluginfile: wordpress-seo/wp-seo => wordpress-seo
        /// Id is needed to install, PluginFile is needed to activate, deactivate, delete
        /// </summary>
        public string Id { get ; set; }

        /// <summary>
        /// The plugin activation status.
        /// </summary>
        [JsonProperty("status")]
        public ActivationStatus Status { get; set; }

        /// <summary>
        /// The plugin name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The plugin's website address.
        /// </summary>
        [JsonProperty("plugin_uri")]
        public string PluginUri { get; set; }

        /// <summary>
        /// The plugin author.
        /// </summary>
        [JsonProperty("author")]
        public string Author { get; set; }

        /// <summary>
        /// Plugin author's website address.
        /// </summary>
        [JsonProperty("author_uri")]
        public string AuthorUri { get; set; }

        /// <summary>
        /// The plugin description.
        /// </summary>
        [JsonProperty("description")]
        public Description Description { get; set; }

        /// <summary>
        /// The plugin version number.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Whether the plugin can only be activated network-wide.
        /// </summary>
        [JsonProperty("network_only")]
        public bool NetworkOnly { get; set; }

        /// <summary>
        /// Minimum required version of WordPress.
        /// </summary>
        [JsonProperty("requires_wp")]
        public string RequiresWordPress { get; set; }

        /// <summary>
        /// Minimum required version of PHP.
        /// </summary>
        [JsonProperty("requires_php")]
        public string RequiresPHP { get; set; }

        /// <summary>
        /// The plugin's text domain.
        /// </summary>
        [JsonProperty("textdomain")]
        public string TextDomain { get; set; }

        /// <summary>
        /// The plugin's links.
        /// </summary>
        [JsonProperty("_links")]
        public Links Links { get; set; }
    }
}