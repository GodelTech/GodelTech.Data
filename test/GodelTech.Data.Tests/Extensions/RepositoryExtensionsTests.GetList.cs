using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GodelTech.Data.Extensions;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetList_WhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.GetList<IEntity<TKey>, TKey>(null, x => x.Id.Equals(defaultKey))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetList_ByFilterExpression_ReturnsEntity<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var list = new List<TEntity>();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetList(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y =>
                                filterExpression == null && y == null
                                || y.Filter.Expression == filterExpression
                                && y.Sort == null
                                && y.Page == null
                        )
                    )
                )
                .Returns(list);

            var repository = mockRepository.Object;

            // Act
            var result = repository.GetList(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(list, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetListModel_WhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.GetList<FakeModel, IEntity<TKey>, TKey>(null, x => x.Id.Equals(defaultKey))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetListModel_ByFilterExpression_ReturnsEntity<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var list = new List<FakeModel>();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetList<FakeModel>(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y =>
                                filterExpression == null && y == null
                                || y.Filter.Expression == filterExpression
                                && y.Sort == null
                                && y.Page == null
                        )
                    )
                )
                .Returns(list);

            var repository = mockRepository.Object;

            // Act
            var result = repository.GetList<FakeModel, TEntity, TKey>(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(list, result);
        }
    }
}