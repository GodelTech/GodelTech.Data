using System;
using Moq;
using Xunit;

namespace GodelTech.Data.Tests
{
    // ReSharper disable once InconsistentNaming
    public class IUnitOfWorkTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public IUnitOfWorkTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
        }

        [Fact]
        public void Inherit_IEquatable()
        {
            // Arrange & Act & Assert
            Assert.IsAssignableFrom<IDisposable>(_mockUnitOfWork.Object);
        }

        [Fact]
        public void Commit_Success()
        {
            // Arrange
            const int result = 1;

            _mockUnitOfWork.Setup(m => m.Commit()).Returns(result);

            // Act & Assert
            Assert.Equal(result, _mockUnitOfWork.Object.Commit());
        }
    }
}