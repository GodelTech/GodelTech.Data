using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    public static partial class RepositoryExtensions
    {
        /// <summary>
        /// Gets entity of type T from repository that satisfies an expression.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>TEntity</cref>.</returns>
        public static TEntity Get<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.Get(
                filterExpression.CreateQueryParameters<TEntity, TKey>()
            );
        }

        /// <summary>
        /// Gets entity of type T from repository by identifier.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><cref>TEntity</cref>.</returns>
        public static TEntity Get<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository, TKey id)
            where TEntity : class, IEntity<TKey>
        {
            return repository.Get(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id)
            );
        }

        /// <summary>
        /// Gets model of type T from repository that satisfies an expression.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>TModel</cref></returns>
        public static TModel Get<TModel, TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.Get<TModel>(
                filterExpression.CreateQueryParameters<TEntity, TKey>()
            );
        }

        /// <summary>
        /// Gets model of type T from repository by identifier.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><cref>TModel</cref></returns>
        public static TModel Get<TModel, TEntity, TKey>(this IRepository<TEntity, TKey> repository, TKey id)
            where TEntity : class, IEntity<TKey>
        {
            return repository.Get<TModel, TEntity, TKey>(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id)
            );
        }

        /// <summary>
        /// Asynchronously gets entity of type T from repository that satisfies an expression.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><cref>Task{TEntity}</cref>.</returns>
        public static Task<TEntity> GetAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetInternalAsync(filterExpression, cancellationToken);
        }

        /// <summary>
        /// Asynchronously gets entity of type T from repository by identifier.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><cref>Task{TEntity}</cref>.</returns>
        public static async Task<TEntity> GetAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            TKey id,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.GetAsync(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id),
                cancellationToken
            );
        }

        /// <summary>
        /// Asynchronously gets model of type T from repository that satisfies an expression.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><cref>Task{TModel}</cref>.</returns>
        public static Task<TModel> GetAsync<TModel, TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetInternalAsync<TModel, TEntity, TKey>(filterExpression, cancellationToken);
        }

        /// <summary>
        /// Asynchronously gets model of type T from repository by identifier.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns><cref>Task{TModel}</cref>.</returns>
        public static async Task<TModel> GetAsync<TModel, TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            TKey id,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.GetAsync<TModel, TEntity, TKey>(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id),
                cancellationToken
            );
        }

        private static async Task<TEntity> GetInternalAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.GetAsync(
                filterExpression.CreateQueryParameters<TEntity, TKey>(),
                cancellationToken
            );
        }

        private static async Task<TModel> GetInternalAsync<TModel, TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.GetAsync<TModel>(
                filterExpression.CreateQueryParameters<TEntity, TKey>(),
                cancellationToken
            );
        }
    }
}
