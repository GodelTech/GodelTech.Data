using System;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace GodelTech.Data
{
    /// <summary>
    /// Sort rule class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TType">The type of the T type.</typeparam>
    public class SortRule<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
        /// <summary>
        /// Gets or sets sort order.
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// Gets or sets sort expression.
        /// </summary>
        public Expression<Func<TEntity, object>> Expression { get; set; }
    }
}