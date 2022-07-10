using System.Threading;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Checks if any entity of type T satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        bool Exists(QueryParameters<TEntity, TKey> queryParameters = null);

        /// <summary>
        /// Asynchronously checks if any entity of type T satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        Task<bool> ExistsAsync(QueryParameters<TEntity, TKey> queryParameters = null, CancellationToken cancellationToken = default);
    }
}
