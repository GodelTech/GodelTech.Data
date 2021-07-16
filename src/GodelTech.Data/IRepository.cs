using System.Collections.Generic;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    /// <summary>
    /// Interface of repository for data layer.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TType">The type of the T type.</typeparam>
    public interface IRepository<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
        /// <summary>
        /// Gets entity of type T from repository that satisfies a query parameters.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>TEntity</cref>.</returns>
#pragma warning disable CA1716 // Identifiers should not match keywords
        TEntity Get(QueryParameters<TEntity, TType> queryParameters = null);
#pragma warning restore CA1716 // Identifiers should not match keywords

        /// <summary>
        /// Gets model of type T from repository that satisfies a query parameters.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>TModel</cref></returns>
#pragma warning disable CA1716 // Identifiers should not match keywords
        TModel Get<TModel>(QueryParameters<TEntity, TType> queryParameters = null);
#pragma warning restore CA1716 // Identifiers should not match keywords

        /// <summary>
        /// Asynchronously gets entity of type T from repository that satisfies a query parameters.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{TEntity}</cref>.</returns>
        Task<TEntity> GetAsync(QueryParameters<TEntity, TType> queryParameters = null);

        /// <summary>
        /// Asynchronously gets model of type T from repository that satisfies a query parameters.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{TModel}</cref>.</returns>
        Task<TModel> GetAsync<TModel>(QueryParameters<TEntity, TType> queryParameters = null);

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

        /// <summary>
        /// Gets paged list of entities of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>PagedResult{TEntity}</cref>.</returns>
        PagedResult<TEntity> GetPagedList(QueryParameters<TEntity, TType> queryParameters);

        /// <summary>
        /// Gets paged list of models of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>PagedResult{TModel}</cref>.</returns>
        PagedResult<TModel> GetPagedList<TModel>(QueryParameters<TEntity, TType> queryParameters);

        /// <summary>
        /// Asynchronously gets paged list of entities of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{PagedResult{TEntity}}</cref>.</returns>
        Task<PagedResult<TEntity>> GetPagedListAsync(QueryParameters<TEntity, TType> queryParameters);

        /// <summary>
        /// Asynchronously gets paged list of models of type T from repository that satisfies a query parameters.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns><cref>Task{PagedResult{TModel}}</cref>.</returns>
        Task<PagedResult<TModel>> GetPagedListAsync<TModel>(QueryParameters<TEntity, TType> queryParameters);

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

        /// <summary>
        /// Returns a number that represents how many entities in repository satisfy a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns>A number that represents how many entities in repository satisfy a query parameters.</returns>
        int Count(QueryParameters<TEntity, TType> queryParameters = null);

        /// <summary>
        /// Asynchronously returns a number that represents how many entities in repository satisfy a query parameters.
        /// </summary>
        /// <param name="queryParameters">Query parameters.</param>
        /// <returns>A number that represents how many entities in repository satisfy a query parameters.</returns>
        Task<int> CountAsync(QueryParameters<TEntity, TType> queryParameters = null);

        /// <summary>
        /// Inserts entity in the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>TEntity.</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts list of entities in the repository.
        /// </summary>
        /// <param name="entities">List of entities</param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Asynchronously inserts entity in the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns><cref>TEntity</cref>.</returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Asynchronously inserts list of entities in the repository.
        /// </summary>
        /// <param name="entities">List of entities</param>
        Task InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates entity in the repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="startTrackProperties">if set to <c>true</c> marks entity as modified.</param>
        /// <returns>TEntity.</returns>
        TEntity Update(TEntity entity, bool startTrackProperties = false);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes range of entities by their ids.
        /// </summary>
        /// <param name="ids">List of entities ids.</param>
        void Delete(IEnumerable<TType> ids);
    }
}