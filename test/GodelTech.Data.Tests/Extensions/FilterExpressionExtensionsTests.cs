using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using GodelTech.Data.Extensions;
using GodelTech.Data.Tests.Fakes.Entities;
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
                    new FakeGuidEntity(),
                    default(Guid),
                    Guid.Empty,
                    true
                },
                new object[]
                {
                    new FakeGuidEntity(),
                    default(Guid),
                    new Guid(),
                    true
                },
                new object[]
                {
                    new FakeGuidEntity(),
                    default(Guid),
                    new Guid("00000000-0000-0000-0000-000000000000"),
                    true
                },
                new object[]
                {
                    new FakeGuidEntity(),
                    default(Guid),
                    new Guid("762440ed-9876-4805-b00c-4ae53ba734a4"),
                    false
                },
                new object[]
                {
                    new FakeGuidEntity
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    default(Guid),
                    new Guid("00000000-0000-0000-0000-000000000001"),
                    true
                },
                new object[]
                {
                    new FakeGuidEntity
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    },
                    default(Guid),
                    new Guid("00000000-0000-0000-0000-000000000003"),
                    false
                },
                new object[]
                {
                    new FakeIntEntity(),
                    default(int),
                    0,
                    true
                },
                new object[]
                {
                    new FakeIntEntity(),
                    default(int),
                    1,
                    false
                },
                new object[]
                {
                    new FakeIntEntity
                    {
                        Id = 2
                    },
                    default(int),
                    2,
                    true
                },
                new object[]
                {
                    new FakeIntEntity
                    {
                        Id = 3
                    },
                    default(int),
                    4,
                    false
                },
                new object[]
                {
                    new FakeStringEntity(),
                    string.Empty,
                    null,
                    true
                },
                new object[]
                {
                    new FakeStringEntity(),
                    string.Empty,
                    "",
                    false
                },
                new object[]
                {
                    new FakeStringEntity(),
                    string.Empty,
                    "Test Text",
                    false
                },
                new object[]
                {
                    new FakeStringEntity
                    {
                        Id = "Same Test Text"
                    },
                    string.Empty,
                    "Same Test Text",
                    true
                },
                new object[]
                {
                    new FakeStringEntity
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

        [Fact]
        public void CreateQueryParameters_WhenFilterExpressionIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => FilterExpressionExtensions.CreateQueryParameters<FakeGuidEntity, Guid>(null)
            );

            Assert.Equal("filterExpression", exception.ParamName);
        }

        public static IEnumerable<object[]> NullFilterExpressionMemberData =>
            new Collection<object[]>
            {
                new object[]
                {
                    new FakeGuidEntity(),
                    default(Guid),
                    null
                },
                new object[]
                {
                    new FakeIntEntity(),
                    default(int),
                    null
                },
                new object[]
                {
                    new FakeStringEntity(),
                    string.Empty,
                    null
                }
            };

        public static IEnumerable<object[]> FilterExpressionMemberData =>
            new Collection<object[]>
            {
                new object[]
                {
                    new FakeGuidEntity(),
                    default(Guid),
                    (Expression<Func<FakeGuidEntity, bool>>) (entity => entity.Id == new Guid())
                },
                new object[]
                {
                    new FakeIntEntity(),
                    default(int),
                    (Expression<Func<FakeIntEntity, bool>>) (entity => entity.Id == 1)
                },
                new object[]
                {
                    new FakeStringEntity(),
                    string.Empty,
                    (Expression<Func<FakeStringEntity, bool>>) (entity => entity.Id == "Test Text")
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