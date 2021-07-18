using System.Collections.Generic;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
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
    }
}