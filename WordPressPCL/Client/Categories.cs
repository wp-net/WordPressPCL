using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Categories endpoint WP REST API
    /// </summary>
    public class Categories : CRUDOperation<Category, CategoriesQueryBuilder>
    {
        #region Init

        private const string _methodPath = "categories";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public Categories(HttpHelper HttpHelper) : base(HttpHelper, _methodPath, true)
        {
        }

        #endregion Init
    }
}