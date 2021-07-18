using System;

namespace GodelTech.Data.Extensions
{
    public static partial class RepositoryExtensions
    {
        /// <summary>
        /// Deletes the specified entity by identifier.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        public static void Delete<TEntity, TType>(this IRepository<TEntity, TType> repository, TType id)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            var entity = repository.Get(id);

            if (entity == null) return;

            repository.Delete(entity);
        }
    }
}