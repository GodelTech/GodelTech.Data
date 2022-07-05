using System;
using System.Linq.Expressions;
using GodelTech.Data.Extensions;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Neleus.LambdaCompare;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsTests
    {
        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Get_WhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Get<IEntity<TKey>, TKey>(null, x => x.Id.Equals(defaultKey))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Get_ByFilterExpression_ReturnsEntity<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => y.Filter.Expression == filterExpression
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    )
                )
                .Returns(entity);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Get_ById_ReturnsEntity<TEntity, TKey>(
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
                    x => x.Get(
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
                .Returns(entity);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get((TKey) id);

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
        [MemberData(nameof(FilterExpressionExtensionsTests.TypesMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetModel_WhenRepositoryIsNull_ThrowsArgumentNullException<TKey>(TKey defaultKey)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Get<FakeModel, IEntity<TKey>, TKey>(null, x => x.Id.Equals(defaultKey))
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetModel_ByFilterExpression_ReturnsModel<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var model = new FakeModel();

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get<FakeModel>(
                        It.Is<QueryParameters<TEntity, TKey>>(
                            y => y.Filter.Expression == filterExpression
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    )
                )
                .Returns(model);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get<FakeModel, TEntity, TKey>(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultKey.GetType(), entity.Id);
            }

            Assert.Equal(model, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetModel_ById_ReturnsModel<TEntity, TKey>(
            TKey defaultKey,
            TEntity entity,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TKey>
        {
            // Arrange
            var model = new FakeModel();

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TKey>((TKey) id);

            var mockRepository = new Mock<IRepository<TEntity, TKey>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get<FakeModel>(
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
                .Returns(model);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get<FakeModel, TEntity, TKey>((TKey) id);

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
    }
}
