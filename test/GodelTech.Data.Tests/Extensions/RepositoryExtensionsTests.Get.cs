using System;
using GodelTech.Data.Tests.Fakes;
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
        public void Get_ById_ReturnsEntity<TEntity, TKey>(
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

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get(id);

            // Assert
            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void Get_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Get<IEntity<TKey>, TKey>(null, x => x.Id.Equals(id))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void Get_ByFilterExpression_ReturnsEntity<TKey>(TKey id)
        {
            // Arrange
            var entity = new FakeEntity<TKey>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            mockRepository
                .Setup(
                    x => x.Get(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression)
                    )
                )
                .Returns(entity);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get(filterExpression);

            // Assert
            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void Get_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Get(
                    null,
                    new FakeSpecification<IEntity<TKey>, TKey>(
                        x => x.Id.Equals(id)
                    )
                )
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void Get_BySpecification_ReturnsEntity<TKey>(TKey id)
        {
            // Arrange
            var entity = new FakeEntity<TKey>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            var specification = new FakeSpecification<IEntity<TKey>, TKey>(filterExpression);

            mockRepository
                .Setup(
                    x => x.Get(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression)
                    )
                )
                .Returns(entity);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get(specification);

            // Assert
            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionGuidTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionIntTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionStringTestData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetModel_ById_ReturnsModel<TEntity, TKey>(
            TKey id,
            TEntity entity,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var model = new FakeModel();

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>(id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<TEntity, TKey>(filterExpression)
                    )
                )
                .Returns(model);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get<FakeModel, TEntity, TKey>(id);

            // Assert
            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.Equal(model, result);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetModel_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Get<FakeModel, IEntity<TKey>, TKey>(null, x => x.Id.Equals(id))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetModel_ByFilterExpression_ReturnsModel<TKey>(TKey id)
        {
            // Arrange
            var model = new FakeModel();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            mockRepository
                .Setup(
                    x => x.Get<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression)
                    )
                )
                .Returns(model);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get<FakeModel, IEntity<TKey>, TKey>(filterExpression);

            // Assert
            Assert.Equal(model, result);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetModel_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Get<FakeModel, IEntity<TKey>, TKey>(
                    null,
                    new FakeSpecification<IEntity<TKey>, TKey>(
                        x => x.Id.Equals(id)
                    )
                )
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetModel_BySpecification_ReturnsModel<TKey>(TKey id)
        {
            // Arrange
            var model = new FakeModel();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            var specification = new FakeSpecification<IEntity<TKey>, TKey>(filterExpression);

            mockRepository
                .Setup(
                    x => x.Get<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression)
                    )
                )
                .Returns(model);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get<FakeModel, IEntity<TKey>, TKey>(specification);

            // Assert
            Assert.Equal(model, result);
        }
    }
}
