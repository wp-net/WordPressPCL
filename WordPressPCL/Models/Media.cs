using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WordPressPCL.Models
{
    public class Media
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("date_gmt")]
        public DateTime DateGmt { get; set; }

        [JsonProperty("guid")]
        public Guid Guid { get; set; }

        [JsonProperty("modified")]
        public DateTime Modified { get; set; }

        [JsonProperty("modified_gmt")]
        public DateTime ModifiedGmt { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("author")]
        public int Author { get; set; }

        [JsonProperty("comment_status")]
        public string CommentStatus { get; set; }

        [JsonProperty("ping_status")]
        public string PingStatus { get; set; }

        [JsonProperty("alt_text")]
        public string AltText { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("media_details")]
        public MediaDetails MediaDetails { get; set; }

        [JsonProperty("post")]
        public int Post { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }

    public class Thumbnail
    {

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }
    }

    public class Medium
    {

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }
    }

    public class MediumLarge
    {

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }
    }

    public class Large
    {

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }
    }

    public class PostThumbnail
    {

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }
    }

    public class Full
    {

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }
    }

    public class Sizes
    {

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty("medium")]
        public Medium Medium { get; set; }

        [JsonProperty("medium_large")]
        public MediumLarge MediumLarge { get; set; }

        [JsonProperty("large")]
        public Large Large { get; set; }

        [JsonProperty("post-thumbnail")]
        public PostThumbnail PostThumbnail { get; set; }

        [JsonProperty("full")]
        public Full Full { get; set; }
    }

    public class ImageMeta
    {

        [JsonProperty("aperture")]
        public string Aperture { get; set; }

        [JsonProperty("credit")]
        public string Credit { get; set; }

        [JsonProperty("camera")]
        public string Camera { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("created_timestamp")]
        public string CreatedTimestamp { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("focal_length")]
        public string FocalLength { get; set; }

        [JsonProperty("iso")]
        public string Iso { get; set; }

        [JsonProperty("shutter_speed")]
        public string ShutterSpeed { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("keywords")]
        public IList<string> Keywords { get; set; }
    }

    public class MediaDetails
    {

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("sizes")]
        public Sizes Sizes { get; set; }

        [JsonProperty("image_meta")]
        public ImageMeta ImageMeta { get; set; }
    }


}
