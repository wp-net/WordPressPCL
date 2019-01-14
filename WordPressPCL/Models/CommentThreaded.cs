namespace WordPressPCL.Models
{
    /// <summary>
    /// An extension class for Comment that holds a depth property
    /// for displaying threaded comments
    /// </summary>
    public class CommentThreaded : Comment
    {
        /// <summary>
        /// The depth of a comment
        /// 0 is a top level comments without parent
        /// </summary>
        public int Depth { get; set; }
    }
}