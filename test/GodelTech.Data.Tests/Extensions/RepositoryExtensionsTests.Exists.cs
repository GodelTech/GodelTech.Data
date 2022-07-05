using System;
using System.Linq.Expressions;
using GodelTech.Data.Extensions;
using Moq;
using Neleus.LambdaCompare;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Exists_WhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Exists<IEntity<TKey>, TKey>(null, x => x.Id.Equals(defaultKey))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Exists_ByFilterExpression_ReturnsEntity<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Exists(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y =>
                                (filterExpression == null && y == null)
                                || (y.Filter.Expression == filterExpression
                                && y.Sort == null
                                && y.Page == null)
                        )
                    )
                )
                .Returns(true);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Exists(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Exists_ById_ReturnsEntity<TEntity, TKey>(
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
                    x => x.Exists(
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
                .Returns(true);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Exists((TKey) id);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultKey.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.True(result);
        }
    }
}
