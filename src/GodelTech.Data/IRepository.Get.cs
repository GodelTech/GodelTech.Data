using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Gets entity of type T from repository that satisfies a query parameters.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>TEntity</cref>.</returns>
#pragma warning disable CA1716 // Identifiers should not match keywords
        TEntity Get(QueryParameters<TEntity, TKey> queryParameters = null);
#pragma warning restore CA1716 // Identifiers should not match keywords

        /// <summary>
        /// Gets model of type T from repository that satisfies a query parameters.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>TModel</cref></returns>
#pragma warning disable CA1716 // Identifiers should not match keywords
        TModel Get<TModel>(QueryParameters<TEntity, TKey> queryParameters = null);
#pragma warning restore CA1716 // Identifiers should not match keywords

        /// <summary>
        /// Asynchronously gets entity of type T from repository that satisfies a query parameters.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{TEntity}</cref>.</returns>
        Task<TEntity> GetAsync(QueryParameters<TEntity, TKey> queryParameters = null);

        /// <summary>
        /// Asynchronously gets model of type T from repository that satisfies a query parameters.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{TModel}</cref>.</returns>
        Task<TModel> GetAsync<TModel>(QueryParameters<TEntity, TKey> queryParameters = null);
    }
}