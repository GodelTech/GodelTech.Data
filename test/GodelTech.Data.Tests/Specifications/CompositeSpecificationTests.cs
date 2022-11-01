using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes;
using GodelTech.Data.Tests.Fakes.Specifications;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Specifications
{
    public class CompositeSpecificationTests
    {
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
                    }
                },
                // int
                new object[]
                {
                    default(int),
                    new FakeEntity<int>
                    {
                        Id = 1,
                        Name = "TestName"
                    }
                },
                // string
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>
                    {
                        Id = "TestId",
                        Name = "TestName"
                    }
                }
            };

        [Theory]
        [MemberData(nameof(MemberData))]
        public void AsExpression_Success<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var leftSpecification = new Mock<Specification<TEntity, TKey>>(MockBehavior.Strict);

            var rightSpecification = new Mock<Specification<TEntity, TKey>>(MockBehavior.Strict);

            // Act
            var result = new FakeNullCompositeSpecification<BinaryExpression, TEntity, TKey>(
                leftSpecification.Object,
                rightSpecification.Object
            ).AsExpression();

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Null(result);
        }
    }
}
