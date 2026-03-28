using System.Threading;
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
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Result of operation</returns>
        Task<bool> DeleteAsync(int ID, CancellationToken cancellationToken = default);
    }
}