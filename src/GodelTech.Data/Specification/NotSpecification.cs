using System.Linq.Expressions;
using System;

namespace GodelTech.Data.Specification
{
    internal class NotSpecification<TEntity, TKey> : CompositeSpecification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly ISpecification<TEntity, TKey> _other;

        public NotSpecification(ISpecification<TEntity, TKey> other)
        {
            _other = other;
        }

        public override Expression<Func<TEntity, bool>> AsExpression() =>
            Expression.Lambda<Func<TEntity, bool>>(
                Expression.Not(_other.AsExpression().Body),
                Expression.Parameter(typeof(TEntity)));
    }
}
