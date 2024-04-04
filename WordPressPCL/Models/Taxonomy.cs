﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Type represents Taxonomy Entity of WP REST API
    /// </summary>
    public class Taxonomy
    {
        /// <summary>
        /// All capabilities used by the taxonomy.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("capabilities", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<string, bool> Capabilities { get; set; }

        /// <summary>
        /// A human-readable description of the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Whether or not the taxonomy should have children.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("hierarchical")]
        public bool Hierarchical { get; set; }

        /// <summary>
        /// Human-readable labels for the taxonomy for various contexts.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("labels")]
        public List<string> Labels { get; set; }

        /// <summary>
        /// The title for the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// An alphanumeric identifier for the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Whether or not the term cloud should be displayed.
        /// </summary>
        /// <remarks>Context: edit</remarks>
        [JsonProperty("show_cloud")]
        public bool ShowCloud { get; set; }

        /// <summary>
        /// Types associated with the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit</remarks>
        [JsonProperty("types")]
        public List<string> Types { get; set; }

        /// <summary>
        /// REST base route for the taxonomy.
        /// </summary>
        /// <remarks>Context: view, edit, embed</remarks>
        [JsonProperty("rest_base")]
        public string RestBase { get; set; }

        /// <summary>
        /// Links to related resources
        /// </summary>
        [JsonProperty("_links", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Links Links { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Taxonomy()
        {
        }
    }
}
