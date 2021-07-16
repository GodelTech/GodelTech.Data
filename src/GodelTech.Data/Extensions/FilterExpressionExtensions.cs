using System;
using System.Linq;
using System.Linq.Expressions;

namespace GodelTech.Data.Extensions
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
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns><cref>Expression{Func{TEntity, bool}}</cref>.</returns>
        public static Expression<Func<TEntity, bool>> CreateIdFilterExpression<TEntity, TType>(TType id)
            where TEntity : class, IEntity<TType>
        {
            Expression<Func<TEntity, TType>> property = x => x.Id;

            var leftExpression = property.Body;
            var rightExpression = Expression.Constant(id, typeof(TType));

            return Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(leftExpression, rightExpression),
                property.Parameters.Single()
            );
        }

        /// <summary>s
        /// Creates new query parameters for filter expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns><cref>QueryParameters</cref>.</returns>
        public static QueryParameters<TEntity, TType> CreateQueryParameters<TEntity, TType>(
            this Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            if (filterExpression == null) throw new ArgumentNullException(nameof(filterExpression));

            return new QueryParameters<TEntity, TType>
            {
                Filter = new FilterRule<TEntity, TType>
                {
                    Expression = filterExpression
                }
            };
        }
    }
}