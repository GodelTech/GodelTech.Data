using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes range of entities.
        /// </summary>
        /// <param name="entities">List of entities</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Asynchronously deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously deletes range of entities.
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
