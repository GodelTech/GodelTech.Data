using GodelTech.Data.Specification;
using Xunit;

namespace GodelTech.Data.Tests.Specification
{
    public class AndSpecificationTests
    {
        [Theory]
        [MemberData(nameof(SpecificationTests.MemberData), MemberType = typeof(SpecificationTests))]
        public void IsSatisfiedBy_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            bool leftResult,
            bool rightResult)
            where TEntity : class, IEntity<TKey>
        {
            SpecificationTests.IsSatisfiedBy_Success(
                defaultKey,
                entity,
                leftResult,
                rightResult,
                (left, right) => new AndSpecification<TEntity, TKey>(left, right),
                (left, right) => left.IsSatisfiedBy(entity) && right.IsSatisfiedBy(entity)
            );
        }
    }
}
