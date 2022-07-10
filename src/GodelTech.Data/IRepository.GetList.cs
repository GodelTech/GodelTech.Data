using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Gets entities of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>IList{TEntity}</cref>.</returns>
        IList<TEntity> GetList(QueryParameters<TEntity, TKey> queryParameters = null);

        /// <summary>
        /// Gets models of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>IList{TModel}</cref>.</returns>
        IList<TModel> GetList<TModel>(QueryParameters<TEntity, TKey> queryParameters = null);

        /// <summary>
        /// Asynchronously gets entities of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><cref>Task{IList{TModel}}</cref>.</returns>
        Task<IList<TEntity>> GetListAsync(QueryParameters<TEntity, TKey> queryParameters = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously gets models of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><cref>Task{IList{TModel}}</cref>.</returns>
        Task<IList<TModel>> GetListAsync<TModel>(QueryParameters<TEntity, TKey> queryParameters = null, CancellationToken cancellationToken = default);
    }
}
