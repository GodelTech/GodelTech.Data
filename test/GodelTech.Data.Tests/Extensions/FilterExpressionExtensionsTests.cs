using System;
using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Neleus.LambdaCompare;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public class FilterExpressionExtensionsTests
    {
        public static Expression<Func<TEntity, bool>> GetFilterExpression<TEntity, TKey>(TKey id)
            where TEntity : class, IEntity<TKey>
        {
            return x => x.Id.Equals(id);
        }

        public static QueryParameters<TEntity, TKey> GetMatchingQueryParameters<TEntity, TKey>(Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            if (filterExpression == null)
            {
                return It.Is<QueryParameters<TEntity, TKey>>(
                    x => x == null
                );
            }

            return It.Is<QueryParameters<TEntity, TKey>>(
                x => Lambda.Eq(
                         x.Filter.Expression,
                         filterExpression
                     )
                     && x.Sort == null
                     && x.Page == null
            );
        }

        public static TheoryData<Guid, IEntity<Guid>, bool> CreateIdFilterExpressionGuidTestData =>
            new TheoryData<Guid, IEntity<Guid>, bool>
            {
                {
                    Guid.Empty,
                    new FakeEntity<Guid>(),
                    true
                },
                {
                    new Guid(),
                    new FakeEntity<Guid>(),
                    true
                },
                {
                    new Guid("00000000-0000-0000-0000-000000000000"),
                    new FakeEntity<Guid>(),
                    true
                },
                {
                    new Guid("762440ed-9876-4805-b00c-4ae53ba734a4"),
                    new FakeEntity<Guid>(),
                    false
                },
                {
                    new Guid("00000000-0000-0000-0000-000000000001"),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000001")
                    },
                    true
                },
                {
                    new Guid("00000000-0000-0000-0000-000000000003"),
                    new FakeEntity<Guid>
                    {
                        Id = new Guid("00000000-0000-0000-0000-000000000002")
                    },
                    false
                }
            };

        public static TheoryData<int, IEntity<int>, bool> CreateIdFilterExpressionIntTestData =>
            new TheoryData<int, IEntity<int>, bool>
            {
                {
                    0,
                    new FakeEntity<int>(),
                    true
                },
                {
                    1,
                    new FakeEntity<int>(),
                    false
                },
                {
                    2,
                    new FakeEntity<int>
                    {
                        Id = 2
                    },
                    true
                },
                {
                    4,
                    new FakeEntity<int>
                    {
                        Id = 3
                    },
                    false
                }
            };

        public static TheoryData<string, IEntity<string>, bool> CreateIdFilterExpressionStringTestData =>
            new TheoryData<string, IEntity<string>, bool>
            {
                {
                    string.Empty,
                    new FakeEntity<string>(),
                    false
                },
                {
                    "Test Text",
                    new FakeEntity<string>(),
                    false
                },
                {
                    "Same Test Text",
                    new FakeEntity<string>
                    {
                        Id = "Same Test Text"
                    },
                    true
                },
                {
                    "Other Test Text",
                    new FakeEntity<string>
                    {
                        Id = "Test Text"
                    },
                    false
                }
            };

        [Theory]
        [MemberData(nameof(CreateIdFilterExpressionGuidTestData))]
        [MemberData(nameof(CreateIdFilterExpressionIntTestData))]
        [MemberData(nameof(CreateIdFilterExpressionStringTestData))]
        public void CreateIdFilterExpression<TEntity, TKey>(
            TKey id,
            TEntity entity,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange & Act
            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id);

            // Assert
            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void CreateQueryParameters_WhenFilterExpressionIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => FilterExpressionExtensions.CreateQueryParameters<IEntity<TKey>, TKey>(null)
            );

            Assert.NotNull(id);
            Assert.Equal("filterExpression", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void CreateQueryParameters_ReturnsQueryParameters<TKey>(TKey id)
        {
            // Arrange
            var filterExpression = GetFilterExpression<IEntity<TKey>, TKey>(id);

            // Arrange & Act
            var result = filterExpression.CreateQueryParameters<IEntity<TKey>, TKey>();

            // Assert
            Assert.Equal(result.Filter.Expression, filterExpression);
            Assert.Null(result.Sort);
            Assert.Null(result.Page);
        }
    }
}
