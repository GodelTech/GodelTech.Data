using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Neleus.LambdaCompare;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsAsyncTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_ById_ReturnsEntity<TEntity, TKey>(
            TKey id,
            TEntity entity,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id);

            mockRepository
                .Setup(
                    x => x.GetAsync(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(entity);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync(id, cancellationToken);

            // Assert
            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync<IEntity<TKey>, TKey>(
                    null,
                    x => x.Id.Equals(id)
                )
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_ByFilterExpression_ReturnsEntity<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var entity = new FakeEntity<TKey>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            mockRepository
                .Setup(
                    x => x.GetAsync(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(entity);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync(filterExpression, cancellationToken);

            // Assert
            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync(
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
        public async Task GetAsync_BySpecification_ReturnsEntity<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var entity = new FakeEntity<TKey>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            var specification = new FakeSpecification<IEntity<TKey>, TKey>(filterExpression);

            mockRepository
                .Setup(
                    x => x.GetAsync(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(entity);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync(specification, cancellationToken);

            // Assert
            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_ById_ReturnsModel<TEntity, TKey>(
            TKey id,
            TEntity entity,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var model = new FakeModel();

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetAsync<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(model);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync<FakeModel, TEntity, TKey>(id, cancellationToken);

            // Assert
            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.Equal(model, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync<FakeModel, IEntity<TKey>, TKey>(
                    null,
                    x => x.Id.Equals(id)
                )
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_ByFilterExpression_ReturnsModel<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var model = new FakeModel();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            mockRepository
                .Setup(
                    x => x.GetAsync<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(model);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync<FakeModel, IEntity<TKey>, TKey>(filterExpression, cancellationToken);

            // Assert
            Assert.Equal(model, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync<FakeModel, IEntity<TKey>, TKey>(
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
        public async Task GetModelAsync_BySpecification_ReturnsModel<TKey>(TKey id)
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var model = new FakeModel();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            var specification = new FakeSpecification<IEntity<TKey>, TKey>(filterExpression);

            mockRepository
                .Setup(
                    x => x.GetAsync<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression),
                        cancellationToken
                    )
                )
                .ReturnsAsync(model);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync<FakeModel, IEntity<TKey>, TKey>(specification, cancellationToken);

            // Assert
            Assert.Equal(model, result);
        }
    }
}
