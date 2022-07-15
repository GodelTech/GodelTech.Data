using GodelTech.Data.Specification;
using Xunit;

namespace GodelTech.Data.Tests.Specification
{
    public class NotSpecificationTests
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
                (left, _) => new NotSpecification<TEntity, TKey>(left),
                (left, _) => !left.IsSatisfiedBy(entity)
            );
        }
    }
}
