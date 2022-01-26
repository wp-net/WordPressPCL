using System.Threading.Tasks;

namespace WordPressPCL.Interfaces {

    /// <summary>
    /// Interface for count of Wordpress items
    /// </summary>
    public interface ICountOperation
    {
        /// <summary>
        /// Get Count of Wordpress items
        /// </summary>
        /// <returns>Result of Operation</returns>
        Task<int> GetCountAsync();
    }
}
