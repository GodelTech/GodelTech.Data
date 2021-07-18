using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GodelTech.Data.Extensions
{
    public static partial class RepositoryExtensions
    {
        /// <summary>
        /// Checks if entity of type T that satisfies an expression exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static bool Exists<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.Exists(
                filterExpression?.CreateQueryParameters<TEntity, TType>()
            );
        }

        /// <summary>
        /// Checks if entity of type T with identifier exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static bool Exists<TEntity, TType>(this IRepository<TEntity, TType> repository, TType id)
            where TEntity : class, IEntity<TType>
        {
            return repository.Exists(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>(id)
            );
        }

        /// <summary>
        /// Asynchronously checks if entity of type T that satisfies an expression exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static Task<bool> ExistsAsync<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.ExistsInternalAsync(filterExpression);
        }

        /// <summary>
        /// Asynchronously checks if entity of type T with identifier exists in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        public static async Task<bool> ExistsAsync<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            TType id)
            where TEntity : class, IEntity<TType>
        {
            return await repository.ExistsAsync(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>(id)
            );
        }

        private static async Task<bool> ExistsInternalAsync<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            return await repository.ExistsAsync(
                filterExpression?.CreateQueryParameters<TEntity, TType>()
            );
        }
    }
}