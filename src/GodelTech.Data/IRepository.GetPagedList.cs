using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Gets paged list of entities of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>PagedResult{TEntity}</cref>.</returns>
        PagedResult<TEntity> GetPagedList(QueryParameters<TEntity, TKey> queryParameters);

        /// <summary>
        /// Gets paged list of models of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>PagedResult{TModel}</cref>.</returns>
        PagedResult<TModel> GetPagedList<TModel>(QueryParameters<TEntity, TKey> queryParameters);

        /// <summary>
        /// Asynchronously gets paged list of entities of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{PagedResult{TEntity}}</cref>.</returns>
        Task<PagedResult<TEntity>> GetPagedListAsync(QueryParameters<TEntity, TKey> queryParameters);

        /// <summary>
        /// Asynchronously gets paged list of models of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{PagedResult{TModel}}</cref>.</returns>
        Task<PagedResult<TModel>> GetPagedListAsync<TModel>(QueryParameters<TEntity, TKey> queryParameters);
    }
}