using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    public class CommentCreate
    {
        [JsonProperty("author")]
        public int AuthorId { get; set; }

        [JsonProperty("author_email")]
        public string AuthorEmail { get; set; }

        //[JsonProperty("author_ip")]
        //public string AuthorIP { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_url")]
        public string AuthorUrl { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("post")]
        public int PostId { get; set; }

    }
}
