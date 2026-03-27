
using System;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models
{
    /// <summary>
    /// WordPress Themes
    /// Date: 8 June 2023
    /// Creator: Gregory Liénard
    /// </summary>
    public class Theme
    {
        /// <summary>
        /// The theme's stylesheet. This uniquely identifies the theme.
        /// </summary>
        [JsonPropertyName("stylesheet")]
        public string? Stylesheet { get; set; }

        /// <summary>
        /// The theme's template. If this is a child theme, this refers to the parent theme.
        /// </summary>
        [JsonPropertyName("template")]
        public string? Template { get; set; }

        /// <summary>
        /// The theme author.
        /// </summary>
        [JsonPropertyName("author")]
        public Rendered? Author { get; set; }

        /// <summary>
        /// The website of the theme author.
        /// </summary>
        [JsonPropertyName("author_uri")]
        public Rendered? AuthorUri { get; set; }

        /// <summary>
        /// A description of the theme.
        /// </summary>
        [JsonPropertyName("description")]
        public Description? Description { get; set; }

        /// <summary>
        /// The name of the theme.
        /// </summary>
        [JsonPropertyName("name")]
        public Rendered? Name { get; set; }

        /// <summary>
        /// The minimum PHP version required for the theme to work.
        /// </summary>
        [JsonPropertyName("requires_php")]
        public string? RequiresPhp { get; set; }

        /// <summary>
        /// The minimum WordPress version required for the theme to work.
        /// </summary>
        [JsonPropertyName("requires_wp")]
        public string? RequiresWp { get; set; }

        /// <summary>
        /// The theme's screenshot URL.
        /// </summary>
        [JsonPropertyName("screenshot")]
        public Uri? Screenshot { get; set; }

        /// <summary>
        /// Tags indicating styles and features of the theme.
        /// </summary>
        [JsonPropertyName("tags")]
        public Tags? Tags { get; set; }

        /// <summary>
        /// The theme's text domain.
        /// </summary>
        [JsonPropertyName("textdomain")]
        public string? Textdomain { get; set; }


        /// <summary>
        /// The URI of the theme's webpage.
        /// </summary>
        [JsonPropertyName("theme_uri")]
        public Rendered? ThemeUri { get; set; }

        /// <summary>
        /// The theme's current version.
        /// </summary>
        [JsonPropertyName("version")]
        public Version? Version { get; set; }

        /// <summary>
        /// A named status for the theme.
        /// </summary>
        [JsonPropertyName("status")]
        public ActivationStatus Status { get; set; }
    }

}
