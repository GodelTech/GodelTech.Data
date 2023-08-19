﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public static partial class RepositoryExtensions
    {
        /// <summary>
        /// Deletes the specified entity by identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        public static void Delete<TEntity, TKey>(this IRepository<TEntity, TKey> repository, TKey id)
            where TEntity : class, IEntity<TKey>
        {
            // Stryker disable once statement
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            var entity = repository.Get(id);

            if (entity == null) return;

            repository.Delete(entity);
        }

        /// <summary>
        /// Deletes range of entities by their ids.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="ids">List of entities ids.</param>
        public static void Delete<TEntity, TKey>(this IRepository<TEntity, TKey> repository, IEnumerable<TKey> ids)
            where TEntity : class, IEntity<TKey>
        {
            // Stryker disable once statement
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            var entities = repository.GetList(x => ids.Contains(x.Id));

            if (!entities.Any()) return;

            repository.Delete(entities);
        }

        /// <summary>
        /// Asynchronously deletes the specified entity by identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        public static Task DeleteAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            TKey id,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            // Stryker disable once statement
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.DeleteInternalAsync(id, cancellationToken);
        }

        /// <summary>
        /// Asynchronously deletes range of entities by their ids.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="ids">List of entities ids.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        public static Task DeleteAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            IEnumerable<TKey> ids,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            // Stryker disable once statement
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.DeleteInternalAsync(ids, cancellationToken);
        }

        private static async Task DeleteInternalAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            TKey id,
            CancellationToken cancellationToken)
            where TEntity : class, IEntity<TKey>
        {
            var entity = await repository.GetAsync(id, cancellationToken);

            if (entity == null) return;

            await repository.DeleteAsync(entity, cancellationToken);
        }

        private static async Task DeleteInternalAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            IEnumerable<TKey> ids,
            CancellationToken cancellationToken)
            where TEntity : class, IEntity<TKey>
        {
            var entities = await repository.GetListAsync(x => ids.Contains(x.Id), cancellationToken);

            if (!entities.Any()) return;

            await repository.DeleteAsync(entities, cancellationToken);
        }
    }
}
