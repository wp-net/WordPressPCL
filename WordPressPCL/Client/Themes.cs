using System.Collections.Generic;
using System.Threading.Tasks;
using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Themes endpoint WP REST API
    /// Date: 26 May 2023
    /// Creator: Gregory Liénard
    /// </summary>
    public class Themes : CRUDOperation<Theme, ThemesQueryBuilder>
    {
        #region Init

        private const string _methodPath = "themes";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        public Themes(HttpHelper HttpHelper) : base(HttpHelper, _methodPath)
        {
        }

        #endregion Init

        #region Custom



        /// <summary>
        /// Get themes by search term
        /// </summary>
        /// <param name="activationStatus">active or inactive</param>
        /// <param name="embed">include embed info</param>
        /// <returns>List of posts</returns>
        public Task<List<Theme>> GetThemesByActivationStatusAsync(ActivationStatus activationStatus, bool embed = false)
        {
            // default values
            // int page = 1, int per_page = 10, int offset = 0, Post.OrderBy orderby = Post.OrderBy.date
            return HttpHelper.GetRequestAsync<List<Theme>>(_methodPath.SetQueryParam("status", activationStatus.ToString().ToLower()), embed, true);
        }



        #endregion Custom
    }
}
