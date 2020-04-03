using System.Collections.Generic;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests
{
    // ReSharper disable once InconsistentNaming
    public class IEntityTests
    {
        private readonly Mock<IEntity<int>> _mockEntity;

        public IEntityTests()
        {
            _mockEntity = new Mock<IEntity<int>>(MockBehavior.Strict);
        }

        [Fact]
        public void Inherit_IEquatable()
        {
            // Arrange & Act & Assert
            Assert.IsAssignableFrom<IEqualityComparer<IEntity<int>>>(_mockEntity.Object);
        }

        [Fact]
        public void Id_Get_Success()
        {
            // Arrange
            const int id = 1;

            _mockEntity.SetupGet(m => m.Id).Returns(id);

            // Act & Assert
            Assert.Equal(id, _mockEntity.Object.Id);
        }

        [Fact]
        public void Id_Set_Success()
        {
            // Arrange
            var result = 0;

            const int id = 1;

            _mockEntity.SetupSet(m => m.Id = It.IsAny<int>()).Callback((int value) => { result = value; });

            // Act
            _mockEntity.Object.Id = id;

            // Assert
            Assert.Equal(id, result);
        }
    }
}