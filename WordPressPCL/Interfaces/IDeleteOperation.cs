using System.Net.Http;
using System.Threading.Tasks;

namespace WordPressPCL.Interfaces
{
    /// <summary>
    /// Interface with required Delete methods
    /// </summary>
    public interface IDeleteOperation
    {
        /// <summary>
        /// Delete object by Id
        /// </summary>
        /// <param name="ID">ID ob object to delete</param>
        /// <returns>Result of operation</returns>
        Task<bool> Delete(int ID);
    }
}