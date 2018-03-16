namespace WordPressPCL.Models
{
    /// <summary>
    /// An extension class for Comment that holds a depth porperty
    /// for displaying threaded comments
    /// </summary>
    public class CommentThreaded : Comment
    {
        /// <summary>
        /// The depht of a comment
        /// 0 is a top level comments without parent
        /// </summary>
        public int Depth { get; set; }
    }
}