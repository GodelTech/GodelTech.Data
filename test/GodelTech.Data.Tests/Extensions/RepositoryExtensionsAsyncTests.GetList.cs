using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsAsyncTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListAsync_WhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetListAsync<IEntity<TKey>, TKey>(null, x => x.Id.Equals(defaultKey))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListAsync_ByFilterExpression_ReturnsEntity<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var list = new List<TEntity>();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetListAsync(
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
                .ReturnsAsync(list);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetListAsync(filterExpression, cancellationToken);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListModelAsync_WhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetListAsync<FakeModel, IEntity<TKey>, TKey>(null, x => x.Id.Equals(defaultKey))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListModelAsync_ByFilterExpression_ReturnsEntity<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var list = new List<FakeModel>();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetListAsync<FakeModel>(
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
                .ReturnsAsync(list);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetListAsync<FakeModel, TEntity, TKey>(filterExpression, cancellationToken);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(list, result);
        }
    }
}
