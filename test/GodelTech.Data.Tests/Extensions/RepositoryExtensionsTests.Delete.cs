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
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Delete_ById_WhenEntityIsNull<TEntity, TKey>(
            TKey id,
            TEntity entity,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression)
                    )
                )
                .Returns(() => null);

            mockRepository
                .Setup(x => x.Delete((TEntity) null));

            var repository = mockRepository.Object;

            // Act
            repository.Delete(id);

            // Assert
            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            mockRepository
                .Verify(
                    x => x.Get(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression)
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
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Delete_ById_WhenEntityIsNotNull<TEntity, TKey>(
            TKey id,
            TEntity entity,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression)
                    )
                )
                .Returns(entity);

            mockRepository
                .Setup(x => x.Delete(entity));

            var repository = mockRepository.Object;

            // Act
            repository.Delete(id);

            // Assert
            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            mockRepository
                .Verify(
                    x => x.Get(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression)
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
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Delete_ByIdsListIsEmpty<TKey>(TKey id)
        {
            // Arrange
            var ids = new List<TKey>
            {
                id
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
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Delete_ByIds<TKey>(TKey id)
        {
            // Arrange
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
