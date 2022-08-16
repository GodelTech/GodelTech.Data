using System;

using GodelTech.Data.Specification;
using GodelTech.Data.Tests.Fakes;

using Moq;

using Xunit;

namespace GodelTech.Data.Tests.Specification
{
    public class CompositeSpecificationTests
    {
        [Theory]
        [MemberData(nameof(SpecificationTests.MemberData), MemberType = typeof(SpecificationTests))]
        public void And_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            bool leftResult,
            bool rightResult)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftResult,
                rightResult,
                (specification, other) => specification.And(other),
                (left, right) => new AndSpecification<TEntity, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(SpecificationTests.MemberData), MemberType = typeof(SpecificationTests))]
        public void AndNot_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            bool leftResult,
            bool rightResult)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftResult,
                rightResult,
                (specification, other) => specification.AndNot(other),
                (left, right) => new AndNotSpecification<TEntity, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(SpecificationTests.MemberData), MemberType = typeof(SpecificationTests))]
        public void Or_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            bool leftResult,
            bool rightResult)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftResult,
                rightResult,
                (specification, other) => specification.Or(other),
                (left, right) => new OrSpecification<TEntity, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(SpecificationTests.MemberData), MemberType = typeof(SpecificationTests))]
        public void OrNot_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            bool leftResult,
            bool rightResult)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftResult,
                rightResult,
                (specification, other) => specification.OrNot(other),
                (left, right) => new OrNotSpecification<TEntity, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(SpecificationTests.MemberData), MemberType = typeof(SpecificationTests))]
        public void Not_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            bool leftResult,
            bool rightResult)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftResult,
                rightResult,
                (specification, _) => specification.Not(),
                (left, _) => new NotSpecification<TEntity, TKey>(left)
            );
        }

        private static void Method_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            bool leftResult,
            bool rightResult,
            Func<ISpecification<TEntity, TKey>, ISpecification<TEntity, TKey>, ISpecification<TEntity, TKey>> method,
            Func<ISpecification<TEntity, TKey>, ISpecification<TEntity, TKey>, ISpecification<TEntity, TKey>> expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            if (method == null) throw new ArgumentNullException(nameof(method));

            // Arrange
            var rightSpecification = new Mock<ISpecification<TEntity, TKey>>(MockBehavior.Strict);
            rightSpecification
                .Setup(x => x.AsExpression())
                .Returns(x => rightResult);

            rightSpecification
                .Setup(x => x.IsSatisfiedBy(entity))
                .Returns(rightResult);

            var specification = new FakeCompositeSpecification<TEntity, TKey>(leftResult);

            // Act
            var result = method(specification, rightSpecification.Object);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            var expectedResultObject = expectedResult(specification, rightSpecification.Object);

            Assert.IsType(expectedResultObject.GetType(), result);
            Assert.Equal(
                expectedResultObject.IsSatisfiedBy(entity),
                result.IsSatisfiedBy(entity)
            );
        }
    }
}
