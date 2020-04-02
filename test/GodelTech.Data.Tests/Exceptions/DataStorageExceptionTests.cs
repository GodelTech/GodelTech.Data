using System;
using System.Runtime.Serialization;
using Xunit;

namespace GodelTech.Data.Tests.Exceptions
{
    public class DataStorageExceptionTests
    {
        [Fact]
        public void Inherit_Exception()
        {
            // Arrange
            var dataStorageException = new DataStorageException(new Exception());

            // Act & Assert
            Assert.IsAssignableFrom<Exception>(dataStorageException);
        }

        [Fact]
        public void Constructor_NewItem_Created()
        {
            // Arrange
            const string message = "test exception";

            var exception = new Exception(message);
            var dataStorageException = new DataStorageException(exception);

            // Act & Assert
            Assert.Equal(message, dataStorageException.Message);
            Assert.IsType(exception.GetType(), dataStorageException.InnerException);
        }

        [Fact]
        public void ShouldSerializeException()
        {
            // Arrange
            var context = new StreamingContext(StreamingContextStates.File);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new DataStorageException(null, context));
            Assert.Equal("Value cannot be null. (Parameter 'info')", exception.Message);
        }
    }
}