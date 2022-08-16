using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using GodelTech.Data.Specification;
using GodelTech.Data.Tests.Fakes;

using Moq;

using Xunit;

namespace GodelTech.Data.Tests.Specification
{
    public static class SpecificationTests
    {
        public static IEnumerable<object[]> MemberData =>
            new Collection<object[]>
            {
                // Guid
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    true,
                    true
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    true,
                    false
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    false,
                    true
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    false,
                    false
                },
                // int
                new object[]
                {
                    default(int),
                    new FakeEntity<int>(),
                    true,
                    true
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>(),
                    true,
                    false
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>(),
                    false,
                    true
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>(),
                    false,
                    false
                },
                // string
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    true,
                    true
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    true,
                    false
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    false,
                    true
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    false,
                    false
                }
            };

        public static void IsSatisfiedBy_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            bool leftResult,
            bool rightResult,
            Func<ISpecification<TEntity, TKey>, ISpecification<TEntity, TKey>, CompositeSpecification<TEntity, TKey>> specification,
            Func<ISpecification<TEntity, TKey>, ISpecification<TEntity, TKey>, bool> expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));
            if (expectedResult == null) throw new ArgumentNullException(nameof(expectedResult));

            // Arrange
            var leftSpecification = new Mock<ISpecification<TEntity, TKey>>(MockBehavior.Strict);
            leftSpecification
                .Setup(x => x.AsExpression())
                .Returns(x => leftResult);

            leftSpecification
                .Setup(x => x.IsSatisfiedBy(entity))
                .Returns(leftResult);

            var rightSpecification = new Mock<ISpecification<TEntity, TKey>>(MockBehavior.Strict);
            rightSpecification
                .Setup(x => x.AsExpression())
                .Returns(x => rightResult);

            rightSpecification
                .Setup(x => x.IsSatisfiedBy(entity))
                .Returns(rightResult);

            // Act
            var result = specification(leftSpecification.Object, rightSpecification.Object).IsSatisfiedBy(entity);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(
                expectedResult(leftSpecification.Object, rightSpecification.Object),
                result
            );
        }
    }
}
