using System.Linq.Expressions;

namespace GodelTech.Data.Specifications
{
    internal class AndSpecification<TEntity, TKey> : CompositeSpecification<BinaryExpression, TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        internal AndSpecification(Specification<TEntity, TKey> left, Specification<TEntity, TKey> right)
            : base(left, right)
        {

        }

        protected override BinaryExpression CreateExpressionBody()
        {
            return Expression.And(
                Left.AsExpression().Body,
                Right.AsExpression().Body
            );
        }
    }
}
