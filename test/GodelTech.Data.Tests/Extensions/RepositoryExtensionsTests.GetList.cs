using System;
using System.Collections.Generic;
using GodelTech.Data.Tests.Fakes;
using GodelTech.Data.Tests.TestData;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsTests
    {
        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetList_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.GetList<IEntity<TKey>, TKey>(null, x => x.Id.Equals(id))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetList_ByFilterExpression_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var list = new List<IEntity<TKey>>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            mockRepository
                .Setup(
                    x => x.GetList(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression)
                    )
                )
                .Returns(list);

            var repository = mockRepository.Object;

            // Act
            var result = repository.GetList(filterExpression);

            // Assert
            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetList_ByFilterExpressionWhenFilterExpressionIsNull_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var list = new List<IEntity<TKey>>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetList(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(null)
                    )
                )
                .Returns(list);

            var repository = mockRepository.Object;

            // Act
            var result = repository.GetList(filterExpression: null);

            // Assert
            Assert.NotNull(id);
            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetList_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.GetList(
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
        public void GetList_BySpecification_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var list = new List<IEntity<TKey>>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            var specification = new FakeSpecification<IEntity<TKey>, TKey>(filterExpression);

            mockRepository
                .Setup(
                    x => x.GetList(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression)
                    )
                )
                .Returns(list);

            var repository = mockRepository.Object;

            // Act
            var result = repository.GetList(specification);

            // Assert
            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetListModel_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.GetList<FakeModel, IEntity<TKey>, TKey>(null, x => x.Id.Equals(id))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetListModel_ByFilterExpression_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var list = new List<FakeModel>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            mockRepository
                .Setup(
                    x => x.GetList<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression)
                    )
                )
                .Returns(list);

            var repository = mockRepository.Object;

            // Act
            var result = repository.GetList<FakeModel, IEntity<TKey>, TKey>(filterExpression);

            // Assert
            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetListModel_ByFilterExpressionWhenFilterExpressionIsNull_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var list = new List<FakeModel>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetList<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(null)
                    )
                )
                .Returns(list);

            var repository = mockRepository.Object;

            // Act
            var result = repository.GetList<FakeModel, IEntity<TKey>, TKey>(filterExpression: null);

            // Assert
            Assert.NotNull(id);
            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(TypesTestData.TypesGuidTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesIntTestData), MemberType = typeof(TypesTestData))]
        [MemberData(nameof(TypesTestData.TypesStringTestData), MemberType = typeof(TypesTestData))]
        public void GetListModel_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey id)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.GetList<FakeModel, IEntity<TKey>, TKey>(
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
        public void GetListModel_BySpecification_ReturnsEntities<TKey>(TKey id)
        {
            // Arrange
            var list = new List<FakeModel>();

            var mockRepository = new Mock<IRepository<IEntity<TKey>, TKey>>(MockBehavior.Strict);

            var filterExpression = FilterExpressionExtensionsTests.GetFilterExpression<IEntity<TKey>, TKey>(id);

            var specification = new FakeSpecification<IEntity<TKey>, TKey>(filterExpression);

            mockRepository
                .Setup(
                    x => x.GetList<FakeModel>(
                        FilterExpressionExtensionsTests.GetMatchingQueryParameters<IEntity<TKey>, TKey>(filterExpression)
                    )
                )
                .Returns(list);

            var repository = mockRepository.Object;

            // Act
            var result = repository.GetList<FakeModel, IEntity<TKey>, TKey>(specification);

            // Assert
            Assert.Equal(list, result);
        }
    }
}
