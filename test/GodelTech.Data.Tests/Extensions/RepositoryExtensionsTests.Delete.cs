using System;
using System.Collections.Generic;
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
        public void Delete_ByIdWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Delete<IEntity<TKey>, TKey>(null, defaultKey)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Delete_ById_WhenEntityIsNull<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>((TKey) id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => Lambda.Eq(
                                     y.Filter.Expression,
                                     filterExpression
                                 )
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    )
                )
                .Returns(() => null);

            mockRepository
                .Setup(x => x.Delete((TEntity) null));

            var repository = mockRepository.Object;

            // Act
            repository.Delete((TKey) id);

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
                    x => x.Get(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => Lambda.Eq(
                                     y.Filter.Expression,
                                     filterExpression
                                 )
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    ),
                    Times.Once
                );

            mockRepository
                .Verify(
                    x => x.Delete((TEntity) null),
                    Times.Never
                );
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Delete_ById_WhenEntityIsNotNull<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>((TKey) id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => Lambda.Eq(
                                     y.Filter.Expression,
                                     filterExpression
                                 )
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    )
                )
                .Returns(entity);

            mockRepository
                .Setup(x => x.Delete(entity));

            var repository = mockRepository.Object;

            // Act
            repository.Delete((TKey) id);

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
                    x => x.Get(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => Lambda.Eq(
                                     y.Filter.Expression,
                                     filterExpression
                                 )
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    ),
                    Times.Once
                );

            mockRepository
                .Verify(
                    x => x.Delete(entity),
                    Times.Once
                );
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Delete_ByIdsWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Delete<IEntity<TKey>, TKey>(
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
        public void Delete_ByIdsListIsEmpty<TKey>(TKey defaultKey)
        {
            // Arrange
            var ids = new List<TKey>
            {
                defaultKey
            };

            var mockRepository = new Mock<IRepository<FakeEntity<TKey>, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetList(
                        It.IsAny<QueryParameters<FakeEntity<TKey>, TKey>>()
                    )
                )
                .Returns(new List<FakeEntity<TKey>>());

            var repository = mockRepository.Object;

            // Act
            repository.Delete(ids);

            // Assert
            mockRepository
                .Verify(
                    x => x.GetList(
                        It.IsAny<QueryParameters<FakeEntity<TKey>, TKey>>()
                    ),
                    Times.Once
                );
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Delete_ByIds<TKey>(TKey defaultKey)
        {
            // Arrange
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
                    x => x.GetList(
                        It.IsAny<QueryParameters<FakeEntity<TKey>, TKey>>()
                    )
                )
                .Returns(entities);

            mockRepository
                .Setup(
                    x => x.Delete(entities)
                );

            var repository = mockRepository.Object;

            // Act
            repository.Delete(ids);

            // Assert
            mockRepository
                .Verify(
                    x => x.GetList(
                        It.IsAny<QueryParameters<FakeEntity<TKey>, TKey>>()
                    ),
                    Times.Once
                );

            mockRepository
                .Verify(
                    x => x.Delete(entities),
                    Times.Once
                );
        }
    }
}
