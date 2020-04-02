using System.Collections.Generic;
using System.Threading.Tasks;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public class RepositoryExtensionsAsyncTests
    {
        private readonly Mock<IRepository<FakeEntity, int>> _mockRepository;

        public RepositoryExtensionsAsyncTests()
        {
            _mockRepository = new Mock<IRepository<FakeEntity, int>>(MockBehavior.Strict);
        }

        [Fact]
        public async Task GetAsync_ByPredicate_ReturnEntity()
        {
            // Arrange
            var entity = new FakeEntity();

            _mockRepository
                .Setup(m => m.GetAsync(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .ReturnsAsync(entity);

            // Act
            var result = await _mockRepository.Object.GetAsync(x => x.Id == 1);

            // Assert
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task GetAsync_ById_ReturnEntity()
        {
            // Arrange
            var entity = new FakeEntity();

            _mockRepository
                .Setup(m => m.GetAsync(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .ReturnsAsync(entity);

            // Act
            var result = await _mockRepository.Object.GetAsync(1);

            // Assert
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task GetAsyncTModel_ByPredicate_ReturnModel()
        {
            // Arrange
            var dataMapper = new FakeDataMapper();
            var model = new FakeModel();

            _mockRepository
                .Setup(m =>
                    m.GetAsync<FakeModel>(It.IsAny<IDataMapper>(), It.IsAny<QueryParameters<FakeEntity, int>>()))
                .ReturnsAsync(model);

            // Act
            var result = await _mockRepository.Object.GetAsync<FakeModel, FakeEntity, int>(dataMapper, x => x.Id == 1);

            // Assert
            Assert.Equal(model, result);
        }

        [Fact]
        public async Task GetAsyncTModel_ById_ReturnModel()
        {
            // Arrange
            var dataMapper = new FakeDataMapper();
            var model = new FakeModel();

            _mockRepository
                .Setup(m =>
                    m.GetAsync<FakeModel>(It.IsAny<IDataMapper>(), It.IsAny<QueryParameters<FakeEntity, int>>()))
                .ReturnsAsync(model);

            // Act
            var result = await _mockRepository.Object.GetAsync<FakeModel, FakeEntity, int>(dataMapper, 1);

            // Assert
            Assert.Equal(model, result);
        }

        [Fact]
        public async Task GetListAsync_ByPredicate_ReturnEntities()
        {
            // Arrange
            var entities = new List<FakeEntity>
            {
                new FakeEntity()
            };

            _mockRepository
                .Setup(m => m.GetListAsync(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .ReturnsAsync(entities);

            // Act
            var result = await _mockRepository.Object.GetListAsync(x => x.Id == 1);

            // Assert
            Assert.Equal(entities, result);
        }

        [Fact]
        public async Task GetListAyncTModel_ByPredicate_ReturnModels()
        {
            // Arrange
            var dataMapper = new FakeDataMapper();
            var models = new List<FakeModel>
            {
                new FakeModel()
            };

            _mockRepository
                .Setup(m =>
                    m.GetListAsync<FakeModel>(It.IsAny<IDataMapper>(), It.IsAny<QueryParameters<FakeEntity, int>>()))
                .ReturnsAsync(models);

            // Act
            var result = await _mockRepository.Object.GetListAsync<FakeModel, FakeEntity, int>(dataMapper, x => x.Id == 1);

            // Assert
            Assert.Equal(models, result);
        }

        [Fact]
        public async Task ExistsAsync_ByPredicate_ReturnBoolean()
        {
            // Arrange
            _mockRepository
                .Setup(m => m.ExistsAsync(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.ExistsAsync(x => x.Id == 1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ExistsAsync_ById_ReturnBoolean()
        {
            // Arrange
            _mockRepository
                .Setup(m => m.ExistsAsync(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.ExistsAsync(1);

            // Assert
            Assert.True(result);
        }
    }
}