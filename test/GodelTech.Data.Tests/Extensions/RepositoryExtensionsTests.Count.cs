using System;
using System.Linq.Expressions;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Count_ByFilterExpressionWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Count<IEntity<TKey>, TKey>(null, x => x.Id.Equals(defaultKey))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Count_ByFilterExpression_ReturnsCount<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Count(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y =>
                                (filterExpression == null && y == null)
                                || (y.Filter.Expression == filterExpression
                                && y.Sort == null
                                && y.Page == null)
                        )
                    )
                )
                .Returns(1);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Count(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(1, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Count_BySpecificationWhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Count(
                    null,
                    new FakeSpecification<IEntity<TKey>, TKey>(
                        x => x.Id.Equals(defaultKey)
                    )
                )
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Count_BySpecification_ReturnsCount<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            var specification = new FakeSpecification<TEntity, TKey>(expression);

            mockRepository
                .Setup(
                    x => x.Count(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y =>
                                y.Filter.Expression == expression
                                && y.Sort == null
                                && y.Page == null
                        )
                    )
                )
                .Returns(1);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Count(specification);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(1, result);
        }
    }
}
