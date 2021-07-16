using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GodelTech.Data.Extensions;
using GodelTech.Data.Tests.Fakes;
using GodelTech.Data.Tests.Fakes.Entities;
using Moq;
using Neleus.LambdaCompare;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public class RepositoryExtensionsAsyncTests
    {
        #region GetAsync

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_WhenRepositoryIsNull_ThrowsArgumentNullException<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync<TEntity, TType>(null, filterExpression)
            );

            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_ByFilterExpression_ReturnsEntity<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.GetAsync(
                        It.Is<QueryParameters<TEntity, TType>>(
                            y => y.Filter.Expression == filterExpression
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    )
                )
                .ReturnsAsync(entity);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal(entity, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetAsync_ById_ReturnsEntity<TEntity, TType>(
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
                    x => x.GetAsync(
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
                .ReturnsAsync(entity);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync((TType) id);

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

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_WhenRepositoryIsNull_ThrowsArgumentNullException<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetAsync<FakeModel, TEntity, TType>(null, filterExpression)
            );

            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_ByFilterExpression_ReturnsModel<TEntity, TType>(
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
                    x => x.GetAsync<FakeModel>(
                        It.Is<QueryParameters<TEntity, TType>>(
                            y => y.Filter.Expression == filterExpression
                                 && y.Sort == null
                                 && y.Page == null
                        )
                    )
                )
                .ReturnsAsync(model);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync<FakeModel, TEntity, TType>(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal(model, result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetModelAsync_ById_ReturnsModel<TEntity, TType>(
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
                    x => x.GetAsync<FakeModel>(
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
                .ReturnsAsync(model);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetAsync<FakeModel, TEntity, TType>((TType)id);

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

        #endregion

        #region GetListAsync

        [Fact]
        public async Task GetListAsync_WhenRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetListAsync<FakeIntEntity, int>(null, x => x.Id == 1)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListAsync_ByFilterExpression_ReturnsEntity<TEntity, TType>(
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
                    x => x.GetListAsync(
                        It.Is<QueryParameters<TEntity, TType>>(
                            y =>
                                filterExpression == null && y == null
                                || y.Filter.Expression == filterExpression
                                && y.Sort == null
                                && y.Page == null
                        )
                    )
                )
                .ReturnsAsync(list);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetListAsync(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal(list, result);
        }

        [Fact]
        public async Task GetListModelAsync_WhenRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.GetListAsync<FakeModel, FakeIntEntity, int>(null, x => x.Id == 1)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task GetListModelAsync_ByFilterExpression_ReturnsEntity<TEntity, TType>(
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
                    x => x.GetListAsync<FakeModel>(
                        It.Is<QueryParameters<TEntity, TType>>(
                            y =>
                                filterExpression == null && y == null
                                || y.Filter.Expression == filterExpression
                                && y.Sort == null
                                && y.Page == null
                        )
                    )
                )
                .ReturnsAsync(list);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.GetListAsync<FakeModel, TEntity, TType>(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.Equal(list, result);
        }

        #endregion

        #region ExistsAsync

        [Fact]
        public async Task ExistsAsync_WhenRepositoryIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => RepositoryExtensions.ExistsAsync<FakeIntEntity, int>(null, x => x.Id == 1)
            );

            Assert.Equal("repository", exception.ParamName);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.FilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        [MemberData(nameof(FilterExpressionExtensionsTests.NullFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task ExistsAsync_ByFilterExpression_ReturnsEntity<TEntity, TType>(
            TEntity entity,
            TType defaultValue,
            Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TEntity, TType>>(MockBehavior.Strict);

            mockRepository
                .Setup(
                    x => x.ExistsAsync(
                        It.Is<QueryParameters<TEntity, TType>>(
                            y =>
                                filterExpression == null && y == null
                                || y.Filter.Expression == filterExpression
                                && y.Sort == null
                                && y.Page == null
                        )
                    )
                )
                .ReturnsAsync(true);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.ExistsAsync(filterExpression);

            // Assert
            if (entity != null && entity.Id != null)
            {
                Assert.IsType(defaultValue.GetType(), entity.Id);
            }

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(FilterExpressionExtensionsTests.CreateIdFilterExpressionMemberData), MemberType = typeof(FilterExpressionExtensionsTests))]
        public async Task ExistsAsync_ById_ReturnsEntity<TEntity, TType>(
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
                    x => x.ExistsAsync(
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
                .ReturnsAsync(true);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.ExistsAsync((TType) id);

            // Assert
            if (id != null)
            {
                Assert.IsType(defaultValue.GetType(), id);
            }

            Assert.Equal(
                expectedResult,
                filterExpression.Compile().Invoke(entity)
            );

            Assert.True(result);
        }

        #endregion
    }
}