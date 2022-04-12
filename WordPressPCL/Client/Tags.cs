using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Tags endpoint WP REST API
    /// </summary>
    public class Tags : CRUDOperation<Tag, TagsQueryBuilder>
    {
        #region Init

        private const string _methodPath = "tags";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public Tags(HttpHelper HttpHelper) : base(HttpHelper, _methodPath, true)
        {
        }

        #endregion Init
    }
}