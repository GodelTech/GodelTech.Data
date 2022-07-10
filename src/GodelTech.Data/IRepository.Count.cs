using System.Threading;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Returns a number that represents how many entities in repository satisfy a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns>A number that represents how many entities in repository satisfy a query parameters.</returns>
        int Count(QueryParameters<TEntity, TKey> queryParameters = null);

        /// <summary>
        /// Asynchronously returns a number that represents how many entities in repository satisfy a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A number that represents how many entities in repository satisfy a query parameters.</returns>
        Task<int> CountAsync(QueryParameters<TEntity, TKey> queryParameters = null, CancellationToken cancellationToken = default);
    }
}
