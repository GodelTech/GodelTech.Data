using System;
using System.Linq.Expressions;
using GodelTech.Data.Extensions;
using GodelTech.Data.Tests.Fakes;
using GodelTech.Data.Tests.Fakes.Entities;
using Moq;
using Neleus.LambdaCompare;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public partial class RepositoryExtensionsTests
    {
        [Fact]
        public void Get_WhenRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Get<FakeIntEntity, int>(null, x => x.Id == 1)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Get_ByFilterExpression_ReturnsEntity<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get(
                        It.Is<QueryParameters<TEntity, TType>>(
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
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void Get_ById_ReturnsEntity<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>((TType) id);

            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get(
                        It.Is<QueryParameters<TEntity, TType>>(
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
            var result = repository.Get((TType)id);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultValue.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.Equal(entity, result);
        }

        [Fact]
        public void GetModel_WhenRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(
                () => RepositoryExtensions.Get<FakeModel, FakeIntEntity, int>(null, x => x.Id == 1)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetModel_ByFilterExpression_ReturnsModel<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var model = new FakeModel();

            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get<FakeModel>(
                        It.Is<QueryParameters<TEntity, TType>>(
                            y => y.Filter.Expression == filterExpression
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    )
                )
                .Returns(model);

            var repository = mockRepository.Object;

            // Act
            var result = repository.Get<FakeModel, TEntity, TType>(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal(model, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public void GetModel_ById_ReturnsModel<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            object id,
            bool expectedResult)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var model = new FakeModel();

            var filterExpression = FilterExpressionExtensions.CreateIdFilterExpression<TEntity, TType>((TType)id);

            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.Get<FakeModel>(
                        It.Is<QueryParameters<TEntity, TType>>(
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
            var result = repository.Get<FakeModel, TEntity, TType>((TType)id);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultValue.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.Equal(model, result);
        }
    }
}