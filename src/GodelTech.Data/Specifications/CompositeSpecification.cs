using System;
using System.Linq.Expressions;

namespace GodelTech.Data.Specifications
{
    internal abstract class CompositeSpecification<TExpression, TEntity, TKey> : Specification<TEntity, TKey>
        where TExpression : Expression
        where TEntity : class, IEntity<TKey>
    {
        internal CompositeSpecification(Specification<TEntity, TKey> left, Specification<TEntity, TKey> right)
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
