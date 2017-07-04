using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Adds a "Rendered" and a "Raw" property to all classes derived from this one
    /// </summary>
    public abstract class RenderedRawBase
    {
        [JsonProperty("rendered")]
        public string Rendered { get; set; }
        [JsonProperty("raw")]
        public string Raw { get; set; }
    }

    /// <summary>
    /// Adds an Href property to all classes derived from this one
    /// </summary>
    public abstract class HrefBase
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    /// <summary>
    /// The actual content of the object, Rendered and/or Raw depending on the context
    /// </summary>
    public class Content : RenderedRawBase
	{
        [JsonProperty("protected")]
        public bool IsProtected { get; set; }
        public Content()
        {

        }
        public Content(string Content):this(Content,Content)
        {
            
        }
        public Content(string ContentRaw,string ContentRendered)
        {
            Raw = ContentRaw;
            Rendered = ContentRendered;
        }
    }

    /// <summary>
    /// The globally unique identifier for the object.
    /// </summary>
    /// <remarks>
    /// Read only
    /// Context: view, edit
    /// </remarks>
    public class Guid : RenderedRawBase
    {
    }

    /// <summary>
    /// The title for the object.
    /// </summary>
    /// <remarks>Context: view, edit, embed</remarks>
    public class Title : RenderedRawBase
    {
        public Title()
        {

        }
        public Title(string Title) : this(Title, Title)
        {

        }
        public Title(string TitleRaw, string TitleRendered)
        {
            Raw = TitleRaw;
            Rendered = TitleRendered;
        }
    }

    /// <summary>
    /// The excerpt for the object.
    /// </summary>
    /// <remarks>Context: view, edit, embed</remarks>
    public class Excerpt : RenderedRawBase
    {
        /// <summary>
        /// Can the except be edited?
        /// </summary>
        [JsonProperty("protected")]
        public bool IsProtected { get; set; }
    }

    /// <summary>
    /// URL to revisions
    /// </summary>
	public class VersionHistory : HrefBase
    {
    }

    public class Caption : RenderedRawBase { }

    public class HttpsApiWOrgFeaturedmedia : HrefBase
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

    public class Self : HrefBase { }

    public class Collection : HrefBase { }

    public class About : HrefBase { }

    public class Author : HrefBase
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

    public class Reply : HrefBase
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }


    public class Cury : HrefBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }
    }

    public class WpPostType : HrefBase { }

    public class HttpsApiWOrgAttachment : HrefBase { }

    public class HttpsApiWOrgTerm : HrefBase
    {
        public string Taxonomy { get; set; }

        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

    public class HttpsApiWOrgMeta : HrefBase
    {
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

}