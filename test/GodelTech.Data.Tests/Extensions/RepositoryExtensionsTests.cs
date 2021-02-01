using System.Collections.Generic;
using GodelTech.Data.Extensions;
using GodelTech.Data.Tests.Fakes;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests.Extensions
{
    public class RepositoryExtensionsTests
    {
        private readonly Mock<IRepository<FakeEntity, int>> _mockRepository;

        public RepositoryExtensionsTests()
        {
            _mockRepository = new Mock<IRepository<FakeEntity, int>>(MockBehavior.Strict);
        }

        [Fact]
        public void Get_ByPredicate_ReturnEntity()
        {
            // Arrange
            var entity = new FakeEntity();

            _mockRepository
                .Setup(m => m.Get(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .Returns(entity);

            // Act
            var result = _mockRepository.Object.Get(x => x.Id == 1);

            // Assert
            Assert.Equal(entity, result);
        }

        [Fact]
        public void Get_ById_ReturnEntity()
        {
            // Arrange
            var entity = new FakeEntity();

            _mockRepository
                .Setup(m => m.Get(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .Returns(entity);

            // Act
            var result = _mockRepository.Object.Get(1);

            // Assert
            Assert.Equal(entity, result);
        }

        [Fact]
        public void GetTModel_ByPredicate_ReturnModel()
        {
            // Arrange
            var model = new FakeModel();

            _mockRepository
                .Setup(m =>
                    m.Get<FakeModel>(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .Returns(model);

            // Act
            var result = _mockRepository.Object.Get<FakeModel, FakeEntity, int>(x => x.Id == 1);

            // Assert
            Assert.Equal(model, result);
        }

        [Fact]
        public void GetTModel_ById_ReturnModel()
        {
            // Arrange
            var model = new FakeModel();

            _mockRepository
                .Setup(m =>
                    m.Get<FakeModel>(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .Returns(model);

            // Act
            var result = _mockRepository.Object.Get<FakeModel, FakeEntity, int>(1);

            // Assert
            Assert.Equal(model, result);
        }

        [Fact]
        public void GetList_ByPredicate_ReturnEntities()
        {
            // Arrange
            var entities = new List<FakeEntity>
            {
                new FakeEntity()
            };

            _mockRepository
                .Setup(m => m.GetList(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .Returns(entities);

            // Act
            var result = _mockRepository.Object.GetList(x => x.Id == 1);

            // Assert
            Assert.Equal(entities, result);
        }

        [Fact]
        public void GetListTModel_ByPredicate_ReturnModels()
        {
            // Arrange
            var models = new List<FakeModel>
            {
                new FakeModel()
            };

            _mockRepository
                .Setup(m =>
                    m.GetList<FakeModel>(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .Returns(models);

            // Act
            var result = _mockRepository.Object.GetList<FakeModel, FakeEntity, int>(x => x.Id == 1);

            // Assert
            Assert.Equal(models, result);
        }

        [Fact]
        public void Exists_ByPredicate_ReturnBoolean()
        {
            // Arrange
            _mockRepository
                .Setup(m => m.Exists(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .Returns(true);

            // Act
            var result = _mockRepository.Object.Exists(x => x.Id == 1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Exists_ById_ReturnBoolean()
        {
            // Arrange
            _mockRepository
                .Setup(m => m.Exists(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .Returns(true);

            // Act
            var result = _mockRepository.Object.Exists(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_ById_CallGetAndDelete()
        {
            // Arrange
            var entity = new FakeEntity();

            _mockRepository
                .Setup(m => m.Get(It.IsAny<QueryParameters<FakeEntity, int>>()))
                .Returns(entity);
            _mockRepository
                .Setup(m => m.Delete(It.IsAny<FakeEntity>()))
                .Verifiable();

            // Act
            _mockRepository.Object.Delete(1);

            // Assert
            _mockRepository.Verify(x => x.Delete(It.IsAny<FakeEntity>()), Times.Once);
            _mockRepository.Verify(x => x.Get(It.IsAny<QueryParameters<FakeEntity, int>>()), Times.Once);
        }
    }
}