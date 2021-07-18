using System.Collections.Generic;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
        /// <summary>
        /// Gets entities of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>IList{TEntity}</cref>.</returns>
        IList<TEntity> GetList(QueryParameters<TEntity, TType> queryParameters = null);

        /// <summary>
        /// Gets models of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>IList{TModel}</cref>.</returns>
        IList<TModel> GetList<TModel>(QueryParameters<TEntity, TType> queryParameters = null);

        /// <summary>
        /// Asynchronously gets entities of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{IList{TModel}}</cref>.</returns>
        Task<IList<TEntity>> GetListAsync(QueryParameters<TEntity, TType> queryParameters = null);

        /// <summary>
        /// Asynchronously gets models of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{IList{TModel}}</cref>.</returns>
        Task<IList<TModel>> GetListAsync<TModel>(QueryParameters<TEntity, TType> queryParameters = null);
    }
}