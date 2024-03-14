using System.Collections.Generic;
using System.Threading.Tasks;
using WordPressPCL.Utility;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Interface with required Query methods
    /// </summary>
    /// <typeparam name="TClass">return class type</typeparam>
    /// <typeparam name="TQueryClass">Query Builder class</typeparam>
    public interface IQueryOperation<TClass, TQueryClass> where TQueryClass : QueryBuilder
    {
        /// <summary>
        /// Execute query
        /// </summary>
        /// <param name="queryBuilder">query builder with parameters for query</param>
        /// <param name="useAuth">Is use auth header</param>
        /// <returns>list of filtered objects</returns>
        Task<List<TClass>> QueryAsync(TQueryClass queryBuilder, bool useAuth = false);
    }
}