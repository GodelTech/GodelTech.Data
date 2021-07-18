using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GodelTech.Data.Extensions;
using GodelTech.Data.Tests.Fakes;
using GodelTech.Data.Tests.Fakes.Entities;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsTests
    {
        [Fact]
        public void GetList_WhenRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.GetList<FakeIntEntity, int>(null, x => x.Id == 1)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetList_ByFilterExpression_ReturnsEntity<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var list = new List<TEntity>();

            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetList(
                        It.Is<QueryParameters<TEntity, TType>>(
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
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal(list, result);
        }

        [Fact]
        public void GetListModel_WhenRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.GetList<FakeModel, FakeIntEntity, int>(null, x => x.Id == 1)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetListModel_ByFilterExpression_ReturnsEntity<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var list = new List<FakeModel>();

            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetList<FakeModel>(
                        It.Is<QueryParameters<TEntity, TType>>(
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
            var result = repository.GetList<FakeModel, TEntity, TType>(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal(list, result);
        }
    }
}