using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GodelTech.Data.Extensions
{
    public static partial class RepositoryExtensions
    {
        /// <summary>
        /// Gets entities of type T from repository that satisfies an expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>IList{TEntity}</cref>.</returns>
        public static IList<TEntity> GetList<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetList(
                filterExpression?.CreateQueryParameters<TEntity, TKey>()
            );
        }

        /// <summary>
        /// Gets models of type T from repository that satisfies an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>IList{TModel}</cref>.</returns>
        public static IList<TModel> GetList<TModel, TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetList<TModel>(
                filterExpression?.CreateQueryParameters<TEntity, TKey>()
            );
        }

        /// <summary>
        /// Asynchronously gets entities of type T from repository that satisfies an expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>Task{IList{TModel}}</cref>.</returns>
        public static Task<IList<TEntity>> GetListAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetListInternalAsync(filterExpression);
        }

        /// <summary>
        /// Asynchronously gets models of type T from repository that satisfies an expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the T model.</typeparam>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>Task{IList{TModel}}</cref>.</returns>
        public static Task<IList<TModel>> GetListAsync<TModel, TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TKey>
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            return repository.GetListInternalAsync<TModel, TEntity, TKey>(filterExpression);
        }

        private static async Task<IList<TEntity>> GetListInternalAsync<TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.GetListAsync(
                filterExpression?.CreateQueryParameters<TEntity, TKey>()
            );
        }

        private static async Task<IList<TModel>> GetListInternalAsync<TModel, TEntity, TKey>(
            this IRepository<TEntity, TKey> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TKey>
        {
            return await repository.GetListAsync<TModel>(
                filterExpression?.CreateQueryParameters<TEntity, TKey>()
            );
        }
    }
}