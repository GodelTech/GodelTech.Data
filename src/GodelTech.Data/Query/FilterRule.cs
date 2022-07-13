using System;
using System.Linq.Expressions;

namespace GodelTech.Data
{
    /// <summary>
    /// Filter rule class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    public class FilterRule<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRule{TEntity, TKey}"/> class.
        /// </summary>
        public FilterRule()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRule{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="specification"></param>
        public FilterRule(ISpecification<TEntity, TKey> specification)
        {
            Expression = entity => specification.IsSatisfiedBy(entity);
        }

        /// <summary>
        /// Gets or sets filter expression.
        /// </summary>
        public Expression<Func<TEntity, bool>> Expression { get; set; }
    }
}
