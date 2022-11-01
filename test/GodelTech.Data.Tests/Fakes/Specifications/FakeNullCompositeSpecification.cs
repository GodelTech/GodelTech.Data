using System.Linq.Expressions;
using GodelTech.Data.Specifications;

namespace GodelTech.Data.Tests.Fakes.Specifications
{
    internal class FakeNullCompositeSpecification<TExpression, TEntity, TKey> : CompositeSpecification<TExpression, TEntity, TKey>
        where TExpression : Expression
        where TEntity : class, IEntity<TKey>
    {
        internal FakeNullCompositeSpecification(Specification<TEntity, TKey> left, Specification<TEntity, TKey> right)
            : base(left, right)
        {

        }

        protected override TExpression CreateExpressionBody()
        {
            return null;
        }
    }
}
