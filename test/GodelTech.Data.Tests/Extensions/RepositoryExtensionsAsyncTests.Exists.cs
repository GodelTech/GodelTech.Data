using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GodelTech.Data.Extensions;
using GodelTech.Data.Tests.Fakes.Entities;
using Moq;
using Neleus.LambdaCompare;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsAsyncTests
    {
        [Fact]
        public async Task ExistsAsync_WhenRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.ExistsAsync<FakeIntEntity, int>(null, x => x.Id == 1)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task ExistsAsync_ByFilterExpression_ReturnsEntity<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.ExistsAsync(
                        It.Is<QueryParameters<TEntity, TType>>(
                            y =>
                                filterExpression == null && y == null
                                || y.Filter.Expression == filterExpression
                                && y.Sort == null
                                && y.Page == null
                        )
                    )
                )
                .ReturnsAsync(true);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.ExistsAsync(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task ExistsAsync_ById_ReturnsEntity<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>((TType) id);

            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.ExistsAsync(
                        It.Is<QueryParameters<TEntity, TType>>(
                            y => Lambda.Eq(
                                     y.Filter.Expression,
                                     filterExpression
                                 )
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    )
                )
                .ReturnsAsync(true);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.ExistsAsync((TType) id);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultValue.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.True(result);
        }
    }
}