using System;
using System.Linq.Expressions;

namespace GodelTech.Data.Specification
{
    internal class AndNotSpecification<TEntity, TKey> : CompositeSpecification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly ISpecification<TEntity, TKey> _left;
        private readonly ISpecification<TEntity, TKey> _right;

        public AndNotSpecification(ISpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TEntity, bool>> AsExpression() => Expression.Lambda<Func<TEntity, bool>>(
            Expression.And(
                _left.AsExpression().Body,
                Expression.Not(
                    _right.AsExpression().Body)),
            Expression.Parameter(typeof(TEntity)));
    }
}
