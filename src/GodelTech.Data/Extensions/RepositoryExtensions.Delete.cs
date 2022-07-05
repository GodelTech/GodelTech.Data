using System;
using System.Collections.Generic;
using System.Linq;

namespace GodelTech.Data.Extensions
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
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            var entities = repository.GetList(x => ids.Contains(x.Id));

            if (!entities.Any()) return;

            repository.Delete(entities);
        }
    }
}
