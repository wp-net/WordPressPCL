
namespace WordPressPCL.Models
{
    static public class PostFormat
    {
        /// <summary>
        /// Standard is the default post format in WordPress.
        /// It can be an article, a blog post, or anything the user want it to be. 
        /// A standard post can also be any of the other post formats as well. 
        /// For example, a standard post can have a gallery or a video. 
        /// The user can decide whether they want to use their theme’s built in support
        /// for the formatting and display of a particular post format or they would rather 
        /// use the standard post format.
        /// http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Standard
        {
            get { return "standard"; }
        }
        /// <summary>
        /// Aside is an extra bit of information that a blogger may want to provide
        /// to their readers without writing a standard post about it. 
        /// It could be an external link, reference to a discussion carried out
        /// elsewhere on the web, or an interesting piece of information that
        /// does not fit in the regular scope of the blog’s posts.
        ///Aside in WordPress is one of the supported post formats.
        ///Theme developers can choose to provide support for a post format 
        ///to define its visual representation
        ///http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Aside
        {
            get { return "aside"; }
        }
        /// <summary>
        /// Chat is one of the post formats supported by WordPress.
        /// It is used to display a chat transcript.
        /// http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Chat
        {
            get { return "chat"; }
        }
        /// <summary>
        /// Gallery feature allows you to add multiple images in 
        /// a WordPress post or page. You can add multiple galleries 
        /// in a single post. Gallery is also one of the supported post 
        /// formats so theme developers can add support for it and define
        /// the gallery presentation in their theme. An image gallery 
        /// in WordPress can be inserted using the Add Media button and 
        /// then clicking on “Create Gallery” tab.
        /// A gallery is inserted in the post using a shortcode
        /// http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Gallery
        {
            get { return "gallery"; }
        }
        /// <summary>
        /// A link post format contains a link to a web location. 
        /// Ideally it is used when a user just want to share a link instead of 
        /// writing a post. They can just add a title of the link and the URL or
        /// optionally add their own commentary for the link.
        /// http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Link
        {
            get { return "link"; }
        }
        /// <summary>
        /// An image post format in WordPress is used to display 
        /// a single image or photograph.
        /// http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Image
        {
            get { return "image"; }
        }
        /// <summary>
        /// Quote is one of the post formats supported by WordPress. 
        /// It is used for quotations, specially when a user wants to
        /// just share a quote which is not within a standard post or article.
        /// A user may decide to add or to wrap a quote around <blockquote> HTML tag.
        /// http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Quote
        {
            get { return "quote"; }
        }
        /// <summary>
        /// Status is one of the post formats supported by WordPress.
        /// A status is usually a short, twitter-like, status update.
        /// However, it is not necessary for an status to be short or twitter-like.
        /// A user may choose to write longer status updates.
        /// http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Status
        {
            get { return "status"; }
        }
        /// <summary>
        /// Video is one of the post formats supported by WordPress post formats system.
        /// A post in video post format usually contains a video either embedded from a 
        /// third party video hosting service like YouTube or uploaded and played directly
        /// from WordPress. Since WordPress version 3.6 there is support for native video 
        /// upload and playback.
        /// http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Video
        {
            get { return "video"; }
        }
        /// <summary>
        /// Audio is one of the supported post formats in WordPress post formats system.
        /// A post with the audio post type usually contains an audio file embeded from a 
        /// third party hosting service or uploaded directly through WordPress media uploader.
        /// http://www.wpbeginner.com/glossary/post-formats/
        /// </summary>
        public static string Audio
        {
            get { return "audio"; }
        }
    }
}
