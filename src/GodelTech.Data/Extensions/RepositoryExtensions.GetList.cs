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

            return repository.GetListInternalAsync(filterExpression);
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

            return repository.GetListInternalAsync<TModel, TEntity, TType>(filterExpression);
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

        private static async Task<IList<TModel>> GetListInternalAsync<TModel, TEntity, TType>(
            this IRepository<TEntity, TType> repository,
            Expression<Func<TEntity, bool>> filterExpression = null)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetListAsync<TModel>(
                filterExpression?.CreateQueryParameters<TEntity, TType>()
            );
        }
    }
}