using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public class FilterExpressionExtensionsTests
    {
        public static IEnumerable<object[]> CreateIdFilterExpressionMemberData =>
            new Collection<object[]>
            {
                // Guid
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    Guid.Empty,
                    true
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    new Guid(),
                    true
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    new Guid("00000000-0000-0000-0000-000000000000"),
                    true
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    new Guid("762440ed-9876-4805-b00c-4ae53ba734a4"),
                    false
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    new Guid("00000000-0000-0000-0000-000000000001"),
                    true
                },
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    },
                    new Guid("00000000-0000-0000-0000-000000000003"),
                    false
                },
                // int
                new object[]
                {
                    default(int),
                    new FakeEntity<int>(),
                    0,
                    true
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>(),
                    1,
                    false
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>
                    {
                        Id = 2
                    },
                    2,
                    true
                },
                new object[]
                {
                    default(int),
                    new FakeEntity<int>
                    {
                        Id = 3
                    },
                    4,
                    false
                },
                // string
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    null,
                    true
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    "",
                    false
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    "Test Text",
                    false
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>
                    {
                        Id = "Same Test Text"
                    },
                    "Same Test Text",
                    true
                },
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>
                    {
                        Id = "Test Text"
                    },
                    "Other Test Text",
                    false
                }
            };

        [Theory]
        [MemberData(nameof(CreateIdFilterExpressionMemberData))]
        public void CreateIdFilterExpression<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange & Act
            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>((TKey) id);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultKey.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );
        }

        public static IEnumerable<object[]> TypesMemberData =>
            new Collection<object[]>
            {
                // Guid
                new object[]
                {
                    default(Guid)
                },
                // int
                new object[]
                {
                    default(int)
                },
                // string
                new object[]
                {
                    string.Empty
                }
            };

        [Theory]
        [MemberData(nameof(TypesMemberData))]
        public void CreateQueryParameters_WhenFilterExpressionIsNull_ThrowsArgumentNullException<TKey>(TKey defaultValue)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => FilterExpressionExtensions.CreateQueryParameters<IEntity<TKey>, TKey>(null)
            );

            Assert.NotNull(defaultValue);
            Assert.Equal("filterExpression", exception.ParamName);
        }

        public static IEnumerable<object[]> NullFilterExpressionMemberData =>
            new Collection<object[]>
            {
                // Guid
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    null
                },
                // int
                new object[]
                {
                    default(int),
                    new FakeEntity<int>(),
                    null
                },
                // string
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    null
                }
            };

        public static IEnumerable<object[]> FilterExpressionMemberData =>
            new Collection<object[]>
            {
                // Guid
                new object[]
                {
                    default(Guid),
                    new FakeEntity<Guid>(),
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Id == new Guid())
                },
                // int
                new object[]
                {
                    default(int),
                    new FakeEntity<int>(),
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Id == 1)
                },
                // string
                new object[]
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Id == "Test Text")
                }
            };

        [Theory]
        [MemberData(nameof(FilterExpressionMemberData))]
        public void CreateQueryParameters_ReturnsQueryParameters<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange & Act
            var result = filterExpression.CreateQueryParameters<TEntity, TKey>();

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(result.Filter.Expression, filterExpression);
            Assert.Null(result.Sort);
            Assert.Null(result.Page);
        }
    }
}
