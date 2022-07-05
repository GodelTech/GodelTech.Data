using System.Collections.Generic;

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
    }
}
