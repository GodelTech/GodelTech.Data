using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public static partial class RepositoryExtensions
    {
        /// <summary>
        /// Returns a number that represents how many entities in repository satisfy an expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns>A number that represents how many entities in repository satisfy an expression.</returns>
        public static int Count<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.Count(
                filterExpression?.CreateQueryParameters<TEntity, TKey>()
            );
        }

        /// <summary>
        /// Returns a number that represents how many entities in repository satisfy a specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="specification">The specification.</param>
        /// <returns>A number that represents how many entities in repository satisfy a specification.</returns>
        public static int Count<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Specification<TEntity, TKey> specification)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.Count(
                specification.CreateQueryParameters()
            );
        }

        /// <summary>
        /// Asynchronously returns a number that represents how many entities in repository satisfy an expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A number that represents how many entities in repository satisfy an expression.</returns>
        public static Task<int> CountAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.CountInternalAsync(filterExpression, cancellationToken);
        }

        /// <summary>
        /// Asynchronously returns a number that represents how many entities in repository satisfy a specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A number that represents how many entities in repository satisfy a specification.</returns>
        public static Task<int> CountAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Specification<TEntity, TKey> specification,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.CountInternalAsync(specification, cancellationToken);
        }

        private static async Task<int> CountInternalAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.CountAsync(
                filterExpression?.CreateQueryParameters<TEntity, TKey>(),
                cancellationToken
            );
        }

        private static async Task<int> CountInternalAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Specification<TEntity, TKey> specification,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.CountAsync(
                specification.CreateQueryParameters(),
                cancellationToken
            );
        }
    }
}
