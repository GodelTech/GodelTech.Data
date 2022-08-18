using System;
using System.Linq.Expressions;

namespace GodelTech.Data.Specifications
{
#pragma warning disable S2436 // Reduce the number of generic parameters in the 'CompositeSpecification' class to no more than the 2 authorized.
    internal abstract class CompositeSpecification<TExpression, TEntity, TKey> : Specification<TEntity, TKey>
#pragma warning restore S2436 // Reduce the number of generic parameters in the 'CompositeSpecification' class to no more than the 2 authorized.
        where TExpression : Expression
        where TEntity : class, IEntity<TKey>
    {
        private protected CompositeSpecification(Specification<TEntity, TKey> left, Specification<TEntity, TKey> right)
        {
            Left = left;
            Right = right;
        }

        protected Specification<TEntity, TKey> Left { get; }

        protected Specification<TEntity, TKey> Right { get; }

        public override Expression<Func<TEntity, bool>> AsExpression()
        {
            var parameter = Expression.Parameter(typeof(TEntity));

            var body = CreateExpressionBody();

            body = (TExpression) new ParameterReplacer(parameter).Visit(body);

            if (body == null) return null;

            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }

        protected abstract TExpression CreateExpressionBody();
    }
}
