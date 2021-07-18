using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GodelTech.Data.Extensions
{
    public static partial class RepositoryExtensions
    {
        /// <summary>
        /// Gets entity of type T from repository that satisfies an expression.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>TEntity</cref>.</returns>
        public static TEntity Get<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.Get(
                filterExpression.CreateQueryParameters<TEntity, TType>()
            );
        }

        /// <summary>
        /// Gets entity of type T from repository by identifier.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><cref>TEntity</cref>.</returns>
        public static TEntity Get<TEntity, TType>(
            this IRepository<TEntity, TType> repository, TType id)
            where TEntity : class, IEntity<TType>
        {
            return repository.Get(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>(id)
            );
        }

        /// <summary>
        /// Gets model of type T from repository that satisfies an expression.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>TModel</cref></returns>
        public static TModel Get<TModel, TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.Get<TModel>(
                filterExpression.CreateQueryParameters<TEntity, TType>()
            );
        }

        /// <summary>
        /// Gets model of type T from repository by identifier.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><cref>TModel</cref></returns>
        public static TModel Get<TModel, TEntity, TType>(this IRepository<TEntity, TType> repository, TType id)
            where TEntity : class, IEntity<TType>
        {
            return repository.Get<TModel, TEntity, TType>(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>(id)
            );
        }

        /// <summary>
        /// Asynchronously gets entity of type T from repository that satisfies an expression.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>Task{TEntity}</cref>.</returns>
        public static Task<TEntity> GetAsync<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetInternalAsync(filterExpression);
        }

        /// <summary>
        /// Asynchronously gets entity of type T from repository by identifier.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><cref>Task{TEntity}</cref>.</returns>
        public static async Task<TEntity> GetAsync<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            TType id)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetAsync(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>(id)
            );
        }

        /// <summary>
        /// Asynchronously gets model of type T from repository that satisfies an expression.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>Task{TModel}</cref>.</returns>
        public static Task<TModel> GetAsync<TModel, TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetInternalAsync<TModel, TEntity, TType>(filterExpression);
        }

        /// <summary>
        /// Asynchronously gets model of type T from repository by identifier.
        /// If no entity is found, then null is returned.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><cref>Task{TModel}</cref>.</returns>
        public static async Task<TModel> GetAsync<TModel, TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            TType id)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetAsync<TModel, TEntity, TType>(
                FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>(id)
            );
        }

        private static async Task<TEntity> GetInternalAsync<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetAsync(
                filterExpression.CreateQueryParameters<TEntity, TType>()
            );
        }

        private static async Task<TModel> GetInternalAsync<TModel, TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetAsync<TModel>(
                filterExpression.CreateQueryParameters<TEntity, TType>()
            );
        }
    }
}