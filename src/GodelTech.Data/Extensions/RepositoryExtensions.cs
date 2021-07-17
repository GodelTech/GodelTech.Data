using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GodelTech.Data.Extensions
{
    /// <summary>
    /// Extensions of repository for data layer.
    /// </summary>
    public static class RepositoryExtensions
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

            return GetInternalAsync(repository, filterExpression);
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

            return GetInternalAsync<TModel, TEntity, TType>(repository, filterExpression);
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

        /// <summary>
        /// Gets entities of type T from repository that satisfies an expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>IList{TEntity}</cref>.</returns>
        public static IList<TEntity> GetList<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetList(
                filterExpression?.CreateQueryParameters<TEntity, TType>()
            );
        }

        /// <summary>
        /// Gets models of type T from repository that satisfies an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>IList{TModel}</cref>.</returns>
        public static IList<TModel> GetList<TModel, TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetList<TModel>(
                filterExpression?.CreateQueryParameters<TEntity, TType>()
            );
        }

        /// <summary>
        /// Asynchronously gets entities of type T from repository that satisfies an expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>Task{IList{TModel}}</cref>.</returns>
        public static Task<IList<TEntity>> GetListAsync<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return GetListInternalAsync(repository, filterExpression);
        }

        private static async Task<IList<TEntity>> GetListInternalAsync<TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetListAsync(
                filterExpression?.CreateQueryParameters<TEntity, TType>()
            );
        }

        /// <summary>
        /// Asynchronously gets models of type T from repository that satisfies an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>Task{IList{TModel}}</cref>.</returns>
        public static Task<IList<TModel>> GetListAsync<TModel, TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return GetListInternalAsync<TModel, TEntity, TType>(repository, filterExpression);
        }

        private static async Task<IList<TModel>> GetListInternalAsync<TModel, TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetListAsync<TModel>(
                filterExpression?.CreateQueryParameters<TEntity, TType>()
            );
        }

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

            return ExistsInternalAsync(repository, filterExpression);
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