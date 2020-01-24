using System;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace GodelTech.Data
{
    /// <summary>
    /// Filter rule class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TType">The type of the T type.</typeparam>
    public class FilterRule<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
        /// <summary>
        /// Gets or sets filter expression.
        /// </summary>
        public Expression<Func<TEntity, bool>> Expression { get; set; }
    }
}