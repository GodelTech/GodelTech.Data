using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Neleus.LambdaCompare;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task DeleteAsync_ByIdWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.DeleteAsync<IEntity<TKey>, TKey>(null, defaultKey)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task DeleteAsync_ById_WhenEntityIsNull<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>((TKey) id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetAsync(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => Lambda.Eq(
                                     y.Filter.Expression,
                                     filterExpression
                                 )
                                 && y.Sort == null
                                 && y.Page == null
                        ),
                        cancellationToken
                    )
                )
                .ReturnsAsync(() => null);

            mockRepository
                .Setup(x => x.DeleteAsync((TEntity) null, cancellationToken))
                .Returns(Task.CompletedTask);

            var repository = mockRepository.Object;

            // Act
            await repository.DeleteAsync((TKey) id, cancellationToken);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultKey.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            mockRepository
                .Verify(
                    x => x.GetAsync(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => Lambda.Eq(
                                     y.Filter.Expression,
                                     filterExpression
                                 )
                                 && y.Sort == null
                                 && y.Page == null
                        ),
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
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task DeleteAsync_ById_WhenEntityIsNotNull<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>((TKey) id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetAsync(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => Lambda.Eq(
                                     y.Filter.Expression,
                                     filterExpression
                                 )
                                 && y.Sort == null
                                 && y.Page == null
                        ),
                        cancellationToken
                    )
                )
                .ReturnsAsync(entity);

            mockRepository
                .Setup(x => x.DeleteAsync(entity, cancellationToken))
                .Returns(Task.CompletedTask);

            var repository = mockRepository.Object;

            // Act
            await repository.DeleteAsync((TKey) id, cancellationToken);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultKey.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            mockRepository
                .Verify(
                    x => x.GetAsync(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => Lambda.Eq(
                                     y.Filter.Expression,
                                     filterExpression
                                 )
                                 && y.Sort == null
                                 && y.Page == null
                        ),
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
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task DeleteAsync_ByIdsWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.DeleteAsync<IEntity<TKey>, TKey>(
                    null,
                    new List<TKey>
                    {
                        defaultKey
                    }
                )
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task DeleteAsync_ByIdsListIsEmpty<TKey>(TKey defaultKey)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var ids = new List<TKey>
            {
                defaultKey
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
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task DeleteAsync_ByIds<TKey>(TKey defaultKey)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var ids = new List<TKey>
            {
                defaultKey
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
