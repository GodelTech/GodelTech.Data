﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public partial interface IRepository<TEntity, TKey>
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
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><cref>TEntity</cref>.</returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously inserts list of entities in the repository.
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
