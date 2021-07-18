using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using GodelTech.Data.Extensions;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public class FilterExpressionExtensionsTests
    {
        public static IEnumerable<object[]> CreateIdFilterExpressionMemberData =>
            new Collection<object[]>
            {
                new object[]
                {
                    new FakeEntity<Guid>(),
                    default(Guid),
                    Guid.Empty,
                    true
                },
                new object[]
                {
                    new FakeEntity<Guid>(),
                    default(Guid),
                    new Guid(),
                    true
                },
                new object[]
                {
                    new FakeEntity<Guid>(),
                    default(Guid),
                    new Guid("00000000-0000-0000-0000-000000000000"),
                    true
                },
                new object[]
                {
                    new FakeEntity<Guid>(),
                    default(Guid),
                    new Guid("762440ed-9876-4805-b00c-4ae53ba734a4"),
                    false
                },
                new object[]
                {
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    default(Guid),
                    new Guid("00000000-0000-0000-0000-000000000001"),
                    true
                },
                new object[]
                {
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    },
                    default(Guid),
                    new Guid("00000000-0000-0000-0000-000000000003"),
                    false
                },
                new object[]
                {
                    new FakeEntity<int>(),
                    default(int),
                    0,
                    true
                },
                new object[]
                {
                    new FakeEntity<int>(),
                    default(int),
                    1,
                    false
                },
                new object[]
                {
                    new FakeEntity<int>
                    {
                        Id = 2
                    },
                    default(int),
                    2,
                    true
                },
                new object[]
                {
                    new FakeEntity<int>
                    {
                        Id = 3
                    },
                    default(int),
                    4,
                    false
                },
                new object[]
                {
                    new FakeEntity<string>(),
                    string.Empty,
                    null,
                    true
                },
                new object[]
                {
                    new FakeEntity<string>(),
                    string.Empty,
                    "",
                    false
                },
                new object[]
                {
                    new FakeEntity<string>(),
                    string.Empty,
                    "Test Text",
                    false
                },
                new object[]
                {
                    new FakeEntity<string>
                    {
                        Id = "Same Test Text"
                    },
                    string.Empty,
                    "Same Test Text",
                    true
                },
                new object[]
                {
                    new FakeEntity<string>
                    {
                        Id = "Test Text"
                    },
                    string.Empty,
                    "Other Test Text",
                    false
                }
            };

        [Theory]
        [MemberData(nameof(CreateIdFilterExpressionMemberData))]
        public void CreateIdFilterExpression<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TType>
        {
            // Arrange & Act
            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>((TType) id);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultValue.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );
        }

        public static IEnumerable<object[]> TypesMemberData =>
            new Collection<object[]>
            {
                new object[]
                {
                    default(Guid)
                },
                new object[]
                {
                    default(int)
                },
                new object[]
                {
                    string.Empty
                }
            };

        [Theory]
        [MemberData(nameof(TypesMemberData))]
        public void CreateQueryParameters_WhenFilterExpressionIsNull_ThrowsArgumentNullException<TType>(TType defaultValue)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => FilterExpressionExtensions.CreateQueryParameters<FakeEntity<TType>, TType>(null)
            );

            Assert.NotNull(defaultValue);
            Assert.Equal("filterExpression", exception.ParamName);
        }

        public static IEnumerable<object[]> NullFilterExpressionMemberData =>
            new Collection<object[]>
            {
                new object[]
                {
                    new FakeEntity<Guid>(),
                    default(Guid),
                    null
                },
                new object[]
                {
                    new FakeEntity<int>(),
                    default(int),
                    null
                },
                new object[]
                {
                    new FakeEntity<string>(),
                    string.Empty,
                    null
                }
            };

        public static IEnumerable<object[]> FilterExpressionMemberData =>
            new Collection<object[]>
            {
                new object[]
                {
                    new FakeEntity<Guid>(),
                    default(Guid),
                    (Expression<Func<FakeEntity<Guid>, bool>>) (entity => entity.Id == new Guid())
                },
                new object[]
                {
                    new FakeEntity<int>(),
                    default(int),
                    (Expression<Func<FakeEntity<int>, bool>>) (entity => entity.Id == 1)
                },
                new object[]
                {
                    new FakeEntity<string>(),
                    string.Empty,
                    (Expression<Func<FakeEntity<string>, bool>>) (entity => entity.Id == "Test Text")
                }
            };

        [Theory]
        [MemberData(nameof(FilterExpressionMemberData))]
        public void CreateQueryParameters_ReturnsQueryParameters<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange & Act
            var result = filterExpression.CreateQueryParameters<TEntity, TType>();

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal(result.Filter.Expression, filterExpression);
            Assert.Null(result.Sort);
            Assert.Null(result.Page);
        }
    }
}