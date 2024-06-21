using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GodelTech.Data.Tests.Fakes;
using GodelTech.Data.Tests.TestData;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task DeleteAsync_ById_WhenEntityIsNull<TEntity, TKey>(
            TKey id,
            TEntity entity,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetAsync(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(() => null);

            mockRepository
                .Setup(x => x.DeleteAsync((TEntity) null, cancellationToken))
                .Returns(Task.CompletedTask);

            var repository = mockRepository.Object;

            // Act
            await repository.DeleteAsync(id, cancellationToken);

            // Assert
            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            mockRepository
                .Verify(
                    x => x.GetAsync(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression),
                        cancellationToken
                    ),
                    Times.Once
                );

            mockRepository
                .Verify(
                    x => x.DeleteAsync((TEntity) null, cancellationToken),
                    Times.Never
                );
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task DeleteAsync_ById_WhenEntityIsNotNull<TEntity, TKey>(
            TKey id,
            TEntity entity,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetAsync(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(entity);

            mockRepository
                .Setup(x => x.DeleteAsync(entity, cancellationToken))
                .Returns(Task.CompletedTask);

            var repository = mockRepository.Object;

            // Act
            await repository.DeleteAsync(id, cancellationToken);

            // Assert
            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            mockRepository
                .Verify(
                    x => x.GetAsync(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression),
                        cancellationToken
                    ),
                    Times.Once
                );

            mockRepository
                .Verify(
                    x => x.DeleteAsync(entity, cancellationToken),
                    Times.Once
                );
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public async Task DeleteAsync_ByIdsListIsEmpty<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var ids = new List<TKey>
            {
                id
            };

            var mockRepository = new Mock<IRepository<FakeEntity<TKey>, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetListAsync(
                        It.IsAny<QueryParameters<FakeEntity<TKey>, TKey>>(),
                        cancellationToken
                    )
                )
                .ReturnsAsync(new List<FakeEntity<TKey>>());

            var repository = mockRepository.Object;

            // Act
            await repository.DeleteAsync(ids, cancellationToken);

            // Assert
            mockRepository
                .Verify(
                    x => x.GetListAsync(
                        It.IsAny<QueryParameters<FakeEntity<TKey>, TKey>>(),
                        cancellationToken
                    ),
                    Times.Once
                );
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public async Task DeleteAsync_ByIds<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var ids = new List<TKey>
            {
                id
            };

            var entities = new List<FakeEntity<TKey>>
            {
                new FakeEntity<TKey>()
            };

            var mockRepository = new Mock<IRepository<FakeEntity<TKey>, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetListAsync(
                        It.IsAny<QueryParameters<FakeEntity<TKey>, TKey>>(),
                        cancellationToken
                    )
                )
                .ReturnsAsync(entities);

            mockRepository
                .Setup(
                    x => x.DeleteAsync(entities, cancellationToken)
                )
                .Returns(Task.CompletedTask);

            var repository = mockRepository.Object;

            // Act
            await repository.DeleteAsync(ids, cancellationToken);

            // Assert
            mockRepository
                .Verify(
                    x => x.GetListAsync(
                        It.IsAny<QueryParameters<FakeEntity<TKey>, TKey>>(),
                        cancellationToken
                    ),
                    Times.Once
                );

            mockRepository
                .Verify(
                    x => x.DeleteAsync(entities, cancellationToken),
                    Times.Once
                );
        }
    }
}
