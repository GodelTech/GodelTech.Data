using System.Linq.Expressions;

namespace GodelTech.Data.Specifications
{
    internal class AndNotSpecification<TEntity, TKey> : CompositeSpecification<BinaryExpression, TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        internal AndNotSpecification(Specification<TEntity, TKey> left, Specification<TEntity, TKey> right)
            : base(left, right)
        {

        }

        protected override BinaryExpression CreateExpressionBody()
        {
            return Expression.And(
                Left.AsExpression().Body,
                Expression.Not(
                    Right.AsExpression().Body
                )
            );
        }
    }
}
