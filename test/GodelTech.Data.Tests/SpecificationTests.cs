using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using GodelTech.Data.Specifications;
using GodelTech.Data.Tests.Fakes;
using GodelTech.Data.Tests.Specifications;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests
{
    public class SpecificationTests
    {
        public static IEnumerable<object[]> IsSatisfiedByMemberData =>
            new Collection<object[]>
            {
                // Guid
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000001")),
                    false
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    },
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000001")),
                    false
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000001")),
                    true
                },
                // int
                new object[]
                {
                    default(int),
                    new FakeEntity<int>(),
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Id == 1),
                    false
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>
                    {
                        Id = 2
                    },
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Id == 1),
                    false
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>
                    {
                        Id = 1
                    },
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Id == 1),
                    true
                },
                // string
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Id == "TestId"),
                    false
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>
                    {
                        Id = "Other TestId"
                    },
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Id == "TestId"),
                    false
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>
                    {
                        Id = "TestId"
                    },
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Id == "TestId"),
                    true
                }
            };

        [Theory]
        [MemberData(nameof(IsSatisfiedByMemberData))]
        public void IsSatisfiedBy_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> expression,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var specification = new FakeSpecification<TEntity, TKey>(expression);

            // Act
            var result = specification.IsSatisfiedBy(entity);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [MemberData(nameof(CompositeSpecificationTests.MemberData), MemberType = typeof(CompositeSpecificationTests))]
        public void And_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> leftExpression,
            Expression<Func<TEntity, bool>> rightExpression)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftExpression,
                rightExpression,
                (specification, other) => specification.And(other),
                (left, right) => new AndSpecification<TEntity, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(CompositeSpecificationTests.MemberData), MemberType = typeof(CompositeSpecificationTests))]
        public void AndNot_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> leftExpression,
            Expression<Func<TEntity, bool>> rightExpression)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftExpression,
                rightExpression,
                (specification, other) => specification.AndNot(other),
                (left, right) => new AndNotSpecification<TEntity, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(CompositeSpecificationTests.MemberData), MemberType = typeof(CompositeSpecificationTests))]
        public void Or_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> leftExpression,
            Expression<Func<TEntity, bool>> rightExpression)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftExpression,
                rightExpression,
                (specification, other) => specification.Or(other),
                (left, right) => new OrSpecification<TEntity, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(CompositeSpecificationTests.MemberData), MemberType = typeof(CompositeSpecificationTests))]
        public void OrNot_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> leftExpression,
            Expression<Func<TEntity, bool>> rightExpression)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftExpression,
                rightExpression,
                (specification, other) => specification.OrNot(other),
                (left, right) => new OrNotSpecification<TEntity, TKey>(left, right)
            );
        }

        [Theory]
        [MemberData(nameof(CompositeSpecificationTests.MemberData), MemberType = typeof(CompositeSpecificationTests))]
        public void Not_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> leftExpression,
            Expression<Func<TEntity, bool>> rightExpression)
            where TEntity : class, IEntity<TKey>
        {
            Method_Success(
                defaultKey,
                entity,
                leftExpression,
                rightExpression,
                (specification, _) => specification.Not(),
                (left, _) => new NotSpecification<TEntity, TKey>(left)
            );
        }

        private static void Method_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> leftExpression,
            Expression<Func<TEntity, bool>> rightExpression,
            Func<Specification<TEntity, TKey>, Specification<TEntity, TKey>, Specification<TEntity, TKey>> method,
            Func<Specification<TEntity, TKey>, Specification<TEntity, TKey>, Specification<TEntity, TKey>> expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            if (method == null) throw new ArgumentNullException(nameof(method));

            // Arrange
            var specification = new FakeSpecification<TEntity, TKey>(leftExpression);

            var rightSpecification = new Mock<Specification<TEntity, TKey>>(MockBehavior.Strict);
            rightSpecification
                .Setup(x => x.AsExpression())
                .Returns(rightExpression);

            var expectedResultObject = expectedResult(specification, rightSpecification.Object);

            // Act
            var result = method(specification, rightSpecification.Object);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.IsType(expectedResultObject.GetType(), result);

            Assert.Equal(
                expectedResultObject.AsExpression().Compile().Invoke(entity),
                result.AsExpression().Compile().Invoke(entity)
            );
        }
    }
}
