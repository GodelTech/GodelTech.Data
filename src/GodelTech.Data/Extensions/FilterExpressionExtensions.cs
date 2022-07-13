using System;
using System.Linq;
using System.Linq.Expressions;

namespace GodelTech.Data
{
    /// <summary>
    /// Extensions of filter expression.
    /// </summary>
    public static class FilterExpressionExtensions
    {
        /// <summary>
        /// Creates filter expression by id.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns><cref>Expression{Func{TEntity, bool}}</cref>.</returns>
        public static Expression<Func<TEntity, bool>> CreateIdFilterExpression<TEntity, TKey>(TKey id)
            where TEntity : class, IEntity<TKey>
        {
            Expression<Func<TEntity, TKey>> property = x => x.Id;

            var leftExpression = property.Body;
            var rightExpression = Expression.Constant(id, typeof(TKey));

            return Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(leftExpression, rightExpression),
                property.Parameters.Single()
            );
        }

        /// <summary>s
        /// Creates new query parameters for filter expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>QueryParameters</cref>.</returns>
        public static QueryParameters<TEntity, TKey> CreateQueryParameters<TEntity, TKey>(
            this Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            if (filterExpression == null) throw new ArgumentNullException(nameof(filterExpression));

            return new QueryParameters<TEntity, TKey>
            {
                Filter = new FilterRule<TEntity, TKey>
                {
                    Expression = filterExpression
                }
            };
        }
    }
}
