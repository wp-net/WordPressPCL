using Newtonsoft.Json;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
    /// <summary>
    /// Adds a "Rendered" and a "Raw" property to all classes derived from this one
    /// </summary>
    public abstract class RenderedRawBase
    {
        /// <summary>
        /// Rendered text
        /// </summary>
        [JsonProperty("rendered")]
        public string Rendered { get; set; }

        /// <summary>
        /// Raw HTML text
        /// </summary>
        [JsonProperty("raw")]
        public string Raw { get; set; }
    }

    /// <summary>
    /// Adds a "Rendered" and a "Raw" property to all classes derived from this one
    /// </summary>
    public abstract class RenderedArrayRawBase
    {
        /// <summary>
        /// Rendered text
        /// </summary>
        [JsonProperty("rendered")]
        public string  Rendered { get; set; }

        /// <summary>
        /// Raw HTML text
        /// </summary>
        [JsonProperty("raw")]
        public string [] Raw { get; set; }
    }

    /// <summary>
    /// Adds an Href property to all classes derived from this one
    /// </summary>
    public abstract class HrefBase
    {
        /// <summary>
        /// URL link
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; }
    }

    /// <summary>
    /// The actual content of the object, Rendered and/or Raw depending on the context
    /// </summary>
    public class Content : RenderedRawBase
    {
        /// <summary>
        /// Can content be edited
        /// </summary>
        [JsonProperty("protected")]
        public bool IsProtected { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Content()
        {
        }

        /// <summary>
        /// Constructor with same fields for Raw end Rendered
        /// </summary>
        /// <param name="Content">Text for Raw and rendered Content</param>
        public Content(string Content) : this(Content, Content)
        {
        }

        /// <summary>
        /// Constructor with Raw and Rendered content text fields
        /// </summary>
        /// <param name="ContentRaw">Raw HTML content text</param>
        /// <param name="ContentRendered">Rendered content text</param>
        public Content(string ContentRaw, string ContentRendered)
        {
            Raw = ContentRaw;
            Rendered = ContentRendered;
        }
    }

    /// <summary>
    /// The actual description of the object, Rendered and/or Raw depending on the context
    /// </summary>
    public class Description : RenderedRawBase
    {
        /// <summary>
        /// Can description be edited
        /// </summary>
        [JsonProperty("protected")]
        public bool IsProtected { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Description()
        {
        }

        /// <summary>
        /// Constructor with same fields for Raw end Rendered
        /// </summary>
        /// <param name="Description">Text for Raw and rendered description</param>
        public Description(string Description) : this(Description, Description)
        {
        }

        /// <summary>
        /// Constructor with Raw and Rendered description text fields
        /// </summary>
        /// <param name="DescriptionRaw">Raw HTML description text</param>
        /// <param name="DescriptionRendered">Rendered description text</param>
        public Description(string DescriptionRaw, string DescriptionRendered)
        {
            Raw = DescriptionRaw;
            Rendered = DescriptionRendered;
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
    public class Tags : RenderedArrayRawBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Tags()
        {
        }

        /// <summary>
        /// Constructor with same Raw and rendered author
        /// </summary>
        /// <param name="Tag">Text for author</param>
        public Tags(string [] Tag) : this(Tag, string.Join(",",Tag))
        {
        }

        /// <summary>
        /// Constructor with Raw and rendered text
        /// </summary>
        /// <param name="TagsRaw">Raw HTML text for author</param>
        /// <param name="TagsRendered">Rendered text for author</param>
        public Tags(string[] TagsRaw, string TagsRendered)
        {
            Raw = TagsRaw;
            Rendered = TagsRendered;
        }
    }

    public class ThemeSupports
    {
        [JsonProperty("align-wide")]
        public bool AlignWide { get; set; }

        [JsonProperty("automatic-feed-links")]
        public bool AutomaticFeedLinks { get; set; }

        [JsonProperty("block-templates")]
        public bool BlockTemplates { get; set; }

        [JsonProperty("block-template-parts")]
        public bool BlockTemplateParts { get; set; }

        [JsonProperty("custom-background")]
        public bool CustomBackground { get; set; }

        [JsonProperty("custom-header")]
        public bool CustomHeader { get; set; }

        [JsonProperty("custom-logo")]
        public bool CustomLogo { get; set; }

        [JsonProperty("customize-selective-refresh-widgets")]
        public bool CustomizeSelectiveRefreshWidgets { get; set; }

        [JsonProperty("dark-editor-style")]
        public bool DarkEditorStyle { get; set; }

        [JsonProperty("disable-custom-colors")]
        public bool DisableCustomColors { get; set; }

        [JsonProperty("disable-custom-font-sizes")]
        public bool DisableCustomFontSizes { get; set; }

        [JsonProperty("disable-custom-gradients")]
        public bool DisableCustomGradients { get; set; }

        [JsonProperty("disable-layout-styles")]
        public bool DisableLayoutStyles { get; set; }

        [JsonProperty("editor-color-palette")]
        public bool EditorColorPalette { get; set; }

        [JsonProperty("editor-font-sizes")]
        public bool EditorFontSizes { get; set; }

        [JsonProperty("editor-gradient-presets")]
        public bool EditorGradientPresets { get; set; }

        [JsonProperty("editor-styles")]
        public bool EditorStyles { get; set; }

        [JsonProperty("html5")]
        public List<string> Html5 { get; set; }

        [JsonProperty("formats")]
        public List<string> Formats { get; set; }

        [JsonProperty("post-thumbnails")]
        public bool PostThumbnails { get; set; }

        [JsonProperty("responsive-embeds")]
        public bool ResponsiveEmbeds { get; set; }

        [JsonProperty("title-tag")]
        public bool TitleTag { get; set; }

        [JsonProperty("wp-block-styles")]
        public bool WpBlockStyles { get; set; }
    }


    /// <summary>
    /// The tags for the object.
    /// </summary>
    /// <remarks>Context: view, edit, embed</remarks>
    public class Rendered : RenderedRawBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Rendered()
        {
        }

        /// <summary>
        /// Constructor with same Raw and rendered author
        /// </summary>
        /// <param name="Author">Text for author</param>
        public Rendered(string Author) : this(Author, Author)
        {
        }

        /// <summary>
        /// Constructor with Raw and rendered text
        /// </summary>
        /// <param name="AuthorRaw">Raw HTML text for author</param>
        /// <param name="AuthorRendered">Rendered text for author</param>
        public Rendered(string AuthorRaw, string AuthorRendered)
        {
            Raw = AuthorRaw;
            Rendered = AuthorRendered;
        }
    }

    /// <summary>
    /// The title for the object.
    /// </summary>
    /// <remarks>Context: view, edit, embed</remarks>
    public class Title : RenderedRawBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Title()
        {
        }

        /// <summary>
        /// Constructor with same Raw and rendered title
        /// </summary>
        /// <param name="Title">Text for title</param>
        public Title(string Title) : this(Title, Title)
        {
        }

        /// <summary>
        /// Constructor with Raw and rendered text
        /// </summary>
        /// <param name="TitleRaw">Raw HTML text for title</param>
        /// <param name="TitleRendered">Rendered text for title</param>
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

        /// <summary>
        /// Constructor
        /// </summary>
        public Excerpt()
        {
        }

        /// <summary>
        /// Constructor with same Raw and rendered Excerpt
        /// </summary>
        /// <param name="Excerpt">Text for Excerpt</param>
        public Excerpt(string Excerpt) : this(Excerpt, Excerpt)
        {
        }

        /// <summary>
        /// Constructor with Raw and rendered text
        /// </summary>
        /// <param name="ExcerptRaw">Raw HTML text for Excerpt</param>
        /// <param name="ExcerptRendered">Rendered text for Excerpt</param>
        public Excerpt(string ExcerptRaw, string ExcerptRendered)
        {
            Raw = ExcerptRaw;
            Rendered = ExcerptRendered;
        }
    }

    /// <summary>
    /// URL to revisions
    /// </summary>
	public class VersionHistory : HrefBase
    {
    }

    /// <summary>
    /// Caption
    /// </summary>
    public class Caption : RenderedRawBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Caption()
        {
        }

        /// <summary>
        /// Constructor with the same raw and rendered Caption
        /// </summary>
        /// <param name="Caption">Text for caption</param>
        public Caption(string Caption) : this(Caption, Caption)
        {
        }

        /// <summary>
        /// Constructor with raw and rendered caption
        /// </summary>
        /// <param name="CaptionRaw">Raw text for caption</param>
        /// <param name="CaptionRendered">Rendered text for caption</param>
        public Caption(string CaptionRaw, string CaptionRendered)
        {
            Raw = CaptionRaw;
            Rendered = CaptionRendered;
        }
    }

    /// <summary>
    /// Multimedia http info
    /// </summary>
    public class HttpsApiWOrgFeaturedmedia : HrefBase
    {
        /// <summary>
        /// Has embedded info
        /// </summary>
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

    /// <summary>
    /// Self link
    /// </summary>
    public class Self : HrefBase { }

    /// <summary>
    /// Collection links
    /// </summary>
    public class Collection : HrefBase { }

    /// <summary>
    /// About link
    /// </summary>
    public class About : HrefBase { }

    /// <summary>
    /// Author link
    /// </summary>
    public class Author : HrefBase
    {
        /// <summary>
        /// Have embedded info
        /// </summary>
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

    /// <summary>
    /// Link to reply
    /// </summary>
    public class Reply : HrefBase
    {
        /// <summary>
        /// Has embedded info
        /// </summary>
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

    /// <summary>
    /// Cury link
    /// </summary>
    public class Cury : HrefBase
    {
        /// <summary>
        /// Cury name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Is cury templated
        /// </summary>
        [JsonProperty("templated")]
        public bool Templated { get; set; }
    }

    /// <summary>
    /// Post type link WP
    /// </summary>
    public class WpPostType : HrefBase { }

    /// <summary>
    /// Attachment http Link
    /// </summary>
    public class HttpsApiWOrgAttachment : HrefBase { }

    /// <summary>
    /// Term http link
    /// </summary>
    public class HttpsApiWOrgTerm : HrefBase
    {
        /// <summary>
        /// Taxonomy name
        /// </summary>
        [JsonProperty("taxonomy")]
        public string Taxonomy { get; set; }

        /// <summary>
        /// Has embedded info
        /// </summary>
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }

    /// <summary>
    /// Meta http link
    /// </summary>
    public class HttpsApiWOrgMeta : HrefBase
    {
        /// <summary>
        /// Has embedded info
        /// </summary>
        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }
    }
}