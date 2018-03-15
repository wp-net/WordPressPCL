using Newtonsoft.Json;

namespace WordPressPCL.Models
{
    /// <summary>
    /// WordPress main settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Site title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Site description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Site URL.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// This address is used for admin purposes. If you change this we will send you an email at your new address to confirm it. The new address will not become active until confirmed.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// A city in the same timezone as you.
        /// </summary>
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        /// <summary>
        /// A date format for all date strings.
        /// </summary>
        [JsonProperty("date_format")]
        public string DateFormat { get; set; }

        /// <summary>
        /// A time format for all time strings.
        /// </summary>
        [JsonProperty("time_format")]
        public string TimeFormat { get; set; }

        /// <summary>
        /// A day number of the week that the week should start on.
        /// </summary>
        [JsonProperty("start_of_week")]
        public int StartOfWeek { get; set; }

        /// <summary>
        /// WordPress locale code.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Convert emoticons like :-) and :-P to graphics on display.
        /// </summary>
        [JsonProperty("use_smilies")]
        public bool UseSmilies { get; set; }

        /// <summary>
        /// Default category.
        /// </summary>
        [JsonProperty("default_category")]
        public int DefaultCategory { get; set; }

        /// <summary>
        /// Default post format.
        /// </summary>
        [JsonProperty("default_post_format")]
        public string DefaultPostFormat { get; set; }

        /// <summary>
        /// Blog pages show at most.
        /// </summary>
        [JsonProperty("posts_per_page")]
        public int PostsPerPage { get; set; }

        /// <summary>
        /// Default Ping Status
        /// </summary>
        [JsonProperty("default_ping_status")]
        public OpenStatus DefaultPingStatus { get; set; }

        /// <summary>
        /// Default Commment Status
        /// </summary>
        [JsonProperty("default_comment_status")]
        public OpenStatus DefaultCommentStatus { get; set; }
    }
}