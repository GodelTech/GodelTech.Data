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
        /// <returns>A number that represents how many entities in repository satisfy a query parameters.</returns>
        Task<int> CountAsync(QueryParameters<TEntity, TKey> queryParameters = null);
    }
}