using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Specifications
{
    public abstract class SpecificationBaseTests
    {
        protected abstract Func<bool, bool, bool> Func { get; }

        protected abstract Specification<TEntity, TKey> CreateSpecification<TEntity, TKey>(
            Specification<TEntity, TKey> left,
            Specification<TEntity, TKey> right)
            where TEntity : class, IEntity<TKey>;

        public static IEnumerable<object[]> MemberData =>
            new Collection<object[]>
            {
                // Guid
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000001")),
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Name == "TestName")
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000001")),
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Name == "Other TestName")
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000002")),
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Name == "TestName")
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001"),
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Id == new Guid("00000000-0000-0000-0000-000000000002")),
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Name == "Other TestName")
                },
                // int
                new object[]
                {
                    default(int),
                    new FakeEntity<int>
                    {
                        Id = 1,
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Id == 1),
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Name == "TestName")
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>
                    {
                        Id = 1,
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Id == 1),
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Name == "Other TestName")
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>
                    {
                        Id = 1,
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Id == 2),
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Name == "TestName")
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>
                    {
                        Id = 1,
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Id == 2),
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Name == "Other TestName")
                },
                // string
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>
                    {
                        Id = "TestId",
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Id == "TestId"),
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Name == "TestName")
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>
                    {
                        Id = "TestId",
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Id == "TestId"),
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Name == "Other TestName")
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>
                    {
                        Id = "TestId",
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Id == "OtherTestId"),
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Name == "TestName")
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>
                    {
                        Id = "TestId",
                        Name = "TestName"
                    },
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Id == "OtherTestId"),
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Name == "Other TestName")
                }
            };

        [Theory]
        [MemberData(nameof(MemberData))]
        public void AsExpression_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> leftExpression,
            Expression<Func<TEntity, bool>> rightExpression)
            where TEntity : class, IEntity<TKey>
        {
            if (leftExpression == null) throw new ArgumentNullException(nameof(leftExpression));
            if (rightExpression == null) throw new ArgumentNullException(nameof(rightExpression));

            // Arrange
            var leftSpecification = new Mock<Specification<TEntity, TKey>>(MockBehavior.Strict);
            leftSpecification
                .Setup(x => x.AsExpression())
                .Returns(leftExpression);

            var rightSpecification = new Mock<Specification<TEntity, TKey>>(MockBehavior.Strict);
            rightSpecification
                .Setup(x => x.AsExpression())
                .Returns(rightExpression);

            // Act
            var result = CreateSpecification(leftSpecification.Object, rightSpecification.Object).AsExpression();

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(
                Func
                    .Invoke(
                        leftExpression.Compile().Invoke(entity),
                        rightExpression.Compile().Invoke(entity)
                    ),
                result.Compile().Invoke(entity)
            );
        }
    }
}
