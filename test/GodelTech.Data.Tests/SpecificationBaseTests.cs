using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests
{
    public class SpecificationBaseTests
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
    }
}
