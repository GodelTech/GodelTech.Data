using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
        /// <summary>
        /// Checks if any entity of type T satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        bool Exists(QueryParameters<TEntity, TType> queryParameters = null);

        /// <summary>
        /// Asynchronously checks if any entity of type T satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        Task<bool> ExistsAsync(QueryParameters<TEntity, TType> queryParameters = null);
    }
}