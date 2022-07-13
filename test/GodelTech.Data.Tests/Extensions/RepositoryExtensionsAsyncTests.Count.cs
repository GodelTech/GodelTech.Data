using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsAsyncTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task CountAsync_WhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultValue)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.CountAsync<IEntity<TKey>, TKey>(null, x => x.Id.Equals(defaultValue))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task CountAsync_ByFilterExpression_ReturnsEntity<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.CountAsync(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y =>
                                (filterExpression == null && y == null)
                                || (y.Filter.Expression == filterExpression
                                && y.Sort == null
                                && y.Page == null)
                        ),
                        cancellationToken
                    )
                )
                .ReturnsAsync(1);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.CountAsync(filterExpression, cancellationToken);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(1, result);
        }
    }
}
