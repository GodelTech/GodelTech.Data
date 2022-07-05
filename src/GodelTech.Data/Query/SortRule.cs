using System;
using System.Linq.Expressions;

namespace GodelTech.Data
{
    /// <summary>
    /// Sort rule class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    public class SortRule<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Gets or sets sort order.
        /// </summary>
        public SortOrder SortOrder { get; set; }

        /// <summary>
        /// Gets or sets sort expression.
        /// </summary>
        public Expression<Func<TEntity, object>> Expression { get; set; }

        /// <summary>
        /// Gets a value indicating whether there are valid parameters.
        /// </summary>
        public bool IsValid => Expression != null;
    }
}
