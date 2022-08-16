using System.Linq.Expressions;
using System;

namespace GodelTech.Data.Specification
{
    /// <summary>
    /// Composite Specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    public abstract class CompositeSpecification<TEntity, TKey> : ISpecification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <inheritdoc />
        public bool IsSatisfiedBy(TEntity candidate) => AsExpression().Compile().Invoke(candidate);

        /// <inheritdoc />
        public ISpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> other) => new AndSpecification<TEntity, TKey>(this, other);

        /// <inheritdoc />
        public ISpecification<TEntity, TKey> AndNot(ISpecification<TEntity, TKey> other) => new AndNotSpecification<TEntity, TKey>(this, other);

        /// <inheritdoc />
        public ISpecification<TEntity, TKey> Or(ISpecification<TEntity, TKey> other) => new OrSpecification<TEntity, TKey>(this, other);

        /// <inheritdoc />
        public ISpecification<TEntity, TKey> OrNot(ISpecification<TEntity, TKey> other) => new OrNotSpecification<TEntity, TKey>(this, other);

        /// <inheritdoc />
        public ISpecification<TEntity, TKey> Not() => new NotSpecification<TEntity, TKey>(this);

        /// <inheritdoc />
        public abstract Expression<Func<TEntity, bool>> AsExpression();
    }
}
