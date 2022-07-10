using System;
using System.Linq.Expressions;
using GodelTech.Data.Specification;

namespace GodelTech.Data
{
    /// <summary>
    /// LINQ Specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    public abstract class LinqSpecification<TEntity, TKey> : CompositeSpecification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <inheritdoc />
        public override bool IsSatisfiedBy(TEntity candidate) => AsExpression().Compile().Invoke(candidate);

        /// <summary>
        /// Expression.
        /// </summary>
        /// <returns>Expression.</returns>
        public abstract Expression<Func<TEntity, bool>> AsExpression();
    }
}
