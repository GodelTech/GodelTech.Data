using System.Linq.Expressions;

namespace GodelTech.Data.Specifications
{
    internal class NotSpecification<TEntity, TKey> : CompositeSpecification<UnaryExpression, TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        internal NotSpecification(Specification<TEntity, TKey> specification)
            : base(specification, null)
        {

        }

        protected override UnaryExpression CreateExpressionBody()
        {
            return Expression.Not(
                Left.AsExpression().Body
            );
        }
    }
}
