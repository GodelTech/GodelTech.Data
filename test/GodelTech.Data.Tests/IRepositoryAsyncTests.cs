using System.Collections.Generic;
using System.Threading.Tasks;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests
{
    // ReSharper disable once InconsistentNaming
    public class IRepositoryAsyncTests
    {
        private readonly Mock<IRepository<FakeEntity, int>> _mockRepository;

        public IRepositoryAsyncTests()
        {
            _mockRepository = new Mock<IRepository<FakeEntity, int>>(MockBehavior.Strict);
        }

        [Fact]
        public async Task GetListAsync_ByQueryParameters_ReturnListOfEntity()
        {
            // Arrange
            var queryParameters = new QueryParameters<FakeEntity, int>();
            var entity = new FakeEntity();

            _mockRepository
                .Setup(m => m.GetListAsync(queryParameters))
                .ReturnsAsync(new List<FakeEntity> { entity });

            // Act & Assert
            Assert.Contains(entity, await _mockRepository.Object.GetListAsync(queryParameters));
        }

        [Fact]
        public async Task GetListTModelAsync_ByQueryParameters_ReturnListOfModel()
        {
            // Arrange
            var dataMapper = new FakeDataMapper();
            var queryParameters = new QueryParameters<FakeEntity, int>();
            var model = new FakeModel();

            _mockRepository
                .Setup(m => m.GetListAsync<FakeModel>(dataMapper, queryParameters))
                .ReturnsAsync(new List<FakeModel> { model });

            // Act & Assert
            Assert.Contains(model, await _mockRepository.Object.GetListAsync<FakeModel>(dataMapper, queryParameters));
        }

        [Fact]
        public async Task GetPagedListAsync_ByQueryParameters_ReturnListOfEntity()
        {
            // Arrange
            var queryParameters = new QueryParameters<FakeEntity, int>();
            var pagedResult = new PagedResult<FakeEntity>();

            _mockRepository
                .Setup(m => m.GetPagedListAsync(queryParameters))
                .ReturnsAsync(pagedResult);

            // Act & Assert
            Assert.Equal(pagedResult, await _mockRepository.Object.GetPagedListAsync(queryParameters));
        }

        [Fact]
        public async Task GetPagedListTModelAsync_ByQueryParameters_ReturnListOfModel()
        {
            // Arrange
            var dataMapper = new FakeDataMapper();
            var queryParameters = new QueryParameters<FakeEntity, int>();
            var pagedResult = new PagedResult<FakeModel>();

            _mockRepository
                .Setup(m => m.GetPagedListAsync<FakeModel>(dataMapper, queryParameters))
                .ReturnsAsync(pagedResult);

            // Act & Assert
            Assert.Equal(pagedResult, await _mockRepository.Object.GetPagedListAsync<FakeModel>(dataMapper, queryParameters));
        }

        [Fact]
        public async Task ExistsAsync_ByQueryParameters_Success()
        {
            // Arrange
            var queryParameters = new QueryParameters<FakeEntity, int>();

            _mockRepository.Setup(m => m.ExistsAsync(queryParameters)).ReturnsAsync(true);

            // Act & Assert
            Assert.True(await _mockRepository.Object.ExistsAsync(queryParameters));
        }

        [Fact]
        public async Task CountAsync_ByQueryParameters_Success()
        {
            // Arrange
            var queryParameters = new QueryParameters<FakeEntity, int>();

            _mockRepository.Setup(m => m.CountAsync(queryParameters)).ReturnsAsync(1);

            // Act & Assert
            Assert.Equal(1, await _mockRepository.Object.CountAsync(queryParameters));
        }

        [Fact]
        public async Task InsertAsync_Entity_Success()
        {
            // Arrange
            var entity = new FakeEntity();

            _mockRepository.Setup(m => m.InsertAsync(entity)).ReturnsAsync(entity);

            // Act & Assert
            Assert.Equal(entity, await _mockRepository.Object.InsertAsync(entity));
        }

        [Fact]
        public async Task InsertAsync_Entities_Success()
        {
            // Arrange
            var entities = new List<FakeEntity> { new FakeEntity() };

            _mockRepository.Setup(m => m.InsertAsync(It.IsAny<IEnumerable<FakeEntity>>()))
                .Returns(() => Task.CompletedTask);

            // Act
            await _mockRepository.Object.InsertAsync(entities);

            // Assert
            _mockRepository.Verify(x => x.InsertAsync(It.IsAny<IEnumerable<FakeEntity>>()), Times.Once);
        }
    }
}