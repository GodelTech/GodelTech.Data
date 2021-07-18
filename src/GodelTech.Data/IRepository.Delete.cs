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
        /// Deletes range of entities by their ids.
        /// </summary>
        /// <param name="ids">List of entities ids.</param>
        void Delete(IEnumerable<TKey> ids);
    }
}