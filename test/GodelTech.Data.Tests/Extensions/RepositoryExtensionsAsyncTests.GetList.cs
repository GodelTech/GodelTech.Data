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
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListAsync_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetListAsync<IEntity<TKey>, TKey>(null, x => x.Id.Equals(id))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListAsync_ByFilterExpression_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var list = new List<IEntity<TKey>>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            mockRepository
                .Setup(
                    x => x.GetListAsync(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(list);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetListAsync(filterExpression, cancellationToken);

            // Assert
            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListAsync_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetListAsync(
                    null,
                    new FakeSpecification<IEntity<TKey>, TKey>(x => x.Id.Equals(id))
                )
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListAsync_BySpecification_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var list = new List<IEntity<TKey>>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            var specification = new FakeSpecification<IEntity<TKey>, TKey>(filterExpression);

            mockRepository
                .Setup(
                    x => x.GetListAsync(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(list);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetListAsync(specification, cancellationToken);

            // Assert
            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListModelAsync_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetListAsync<FakeModel, IEntity<TKey>, TKey>(null, x => x.Id.Equals(id))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListModelAsync_ByFilterExpression_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var list = new List<FakeModel>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            mockRepository
                .Setup(
                    x => x.GetListAsync<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(list);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetListAsync<FakeModel, IEntity<TKey>, TKey>(filterExpression, cancellationToken);

            // Assert
            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListModelAsync_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetListAsync<FakeModel, IEntity<TKey>, TKey>(
                    null,
                    new FakeSpecification<IEntity<TKey>, TKey>(x => x.Id.Equals(id))
                )
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListModelAsync_BySpecification_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var list = new List<FakeModel>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            var specification = new FakeSpecification<IEntity<TKey>, TKey>(filterExpression);

            mockRepository
                .Setup(
                    x => x.GetListAsync<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(list);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetListAsync<FakeModel, IEntity<TKey>, TKey>(specification, cancellationToken);

            // Assert
            Assert.Equal(list, result);
        }
    }
}
