using System;
using System.Linq.Expressions;
using GodelTech.Data.Specifications;

namespace GodelTech.Data
{
    /// <summary>
    /// LINQ Specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    public abstract class Specification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Expression.
        /// </summary>
        /// <returns>Expression.</returns>
        public abstract Expression<Func<TEntity, bool>> AsExpression();

        /// <summary>
        /// Checks if candidate satisfies.
        /// </summary>
        /// <param name="candidate">Candidate.</param>
        /// <returns>Boolean result of check.</returns>
        public bool IsSatisfiedBy(TEntity candidate) => AsExpression().Compile().Invoke(candidate);

        /// <summary>
        /// And.
        /// </summary>
        /// <param name="other">Other candidate.</param>
        /// <returns>Specification.</returns>
        public Specification<TEntity, TKey> And(Specification<TEntity, TKey> other) => new AndSpecification<TEntity, TKey>(this, other);

        /// <summary>
        /// And not.
        /// </summary>
        /// <param name="other">Other candidate.</param>
        /// <returns>Specification.</returns>
        public Specification<TEntity, TKey> AndNot(Specification<TEntity, TKey> other) => new AndNotSpecification<TEntity, TKey>(this, other);

        /// <summary>
        /// Or.
        /// </summary>
        /// <param name="other">Other candidate.</param>
        /// <returns>Specification.</returns>
        public Specification<TEntity, TKey> Or(Specification<TEntity, TKey> other) => new OrSpecification<TEntity, TKey>(this, other);

        /// <summary>
        /// Or not.
        /// </summary>
        /// <param name="other">Other candidate.</param>
        /// <returns>Specification.</returns>
        public Specification<TEntity, TKey> OrNot(Specification<TEntity, TKey> other) => new OrNotSpecification<TEntity, TKey>(this, other);

        /// <summary>
        /// Not.
        /// </summary>
        /// <returns>Specification.</returns>
        public Specification<TEntity, TKey> Not() => new NotSpecification<TEntity, TKey>(this);
    }
}
