using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Base class for Models
    /// </summary>
    public class Base
    {
        /// <summary>
        /// Unique identifier for the object.
        /// </summary>
        /// <remarks>
        /// Read only
        /// Context: view, edit, embed
        /// </remarks>
        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Id { get; set; }
    }
}