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
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_ById_ReturnsEntity<TEntity, TKey>(
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

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync((TKey) id, cancellationToken);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultKey.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync<TEntity, TKey>(null, filterExpression)
            );

            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_ByFilterExpression_ReturnsEntity<TEntity, TKey>(
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
                    x => x.GetAsync(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => y.Filter.Expression == filterExpression
                                 && y.Sort == null
                                 && y.Page == null
                        ),
                        cancellationToken
                    )
                )
                .ReturnsAsync(entity);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync(filterExpression, cancellationToken);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync(
                    null,
                    new FakeSpecification<TEntity, TKey>(filterExpression)
                )
            );

            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(SpecificationBaseTests.IsSatisfiedByMemberData), MemberType = typeof(SpecificationBaseTests))]
        public async Task GetAsync_BySpecification_ReturnsEntity<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> expression,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            var specification = new FakeSpecification<TEntity, TKey>(expression);

            mockRepository
                .Setup(
                    x => x.GetAsync(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y =>
                                y.Filter.Expression.Compile().Invoke(entity) == expectedResult
                                && y.Sort == null
                                && y.Page == null
                        ),
                        cancellationToken
                    )
                )
                .ReturnsAsync(entity);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync(specification, cancellationToken);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_ById_ReturnsModel<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var model = new FakeModel();

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>((TKey) id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetAsync<FakeModel>(
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
                .ReturnsAsync(model);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync<FakeModel, TEntity, TKey>((TKey) id, cancellationToken);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultKey.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.Equal(model, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync<FakeModel, TEntity, TKey>(null, filterExpression)
            );

            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_ByFilterExpression_ReturnsModel<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var model = new FakeModel();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetAsync<FakeModel>(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => y.Filter.Expression == filterExpression
                                 && y.Sort == null
                                 && y.Page == null
                        ),
                        cancellationToken
                    )
                )
                .ReturnsAsync(model);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync<FakeModel, TEntity, TKey>(filterExpression, cancellationToken);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(model, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync<FakeModel, TEntity, TKey>(
                    null,
                    new FakeSpecification<TEntity, TKey>(filterExpression)
                )
            );

            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(SpecificationBaseTests.IsSatisfiedByMemberData), MemberType = typeof(SpecificationBaseTests))]
        public async Task GetModelAsync_BySpecification_ReturnsModel<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> expression,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var model = new FakeModel();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            var specification = new FakeSpecification<TEntity, TKey>(expression);

            mockRepository
                .Setup(
                    x => x.GetAsync<FakeModel>(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y =>
                                y.Filter.Expression.Compile().Invoke(entity) == expectedResult
                                && y.Sort == null
                                && y.Page == null
                        ),
                        cancellationToken
                    )
                )
                .ReturnsAsync(model);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync<FakeModel, TEntity, TKey>(specification, cancellationToken);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(model, result);
        }
    }
}
