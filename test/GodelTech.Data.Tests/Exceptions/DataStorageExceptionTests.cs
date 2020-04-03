using System;
using System.Runtime.Serialization;
using GodelTech.Data.Tests.Fakes;
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
        public void Constructor_SerializationInfo_Created()
        {
            // Arrange
            var info = new SerializationInfo(typeof(DataStorageException), new FormatterConverter());
            info.AddValue("ClassName", string.Empty);
            info.AddValue("Message", string.Empty);
            info.AddValue("InnerException", new ArgumentException());
            info.AddValue("HelpURL", string.Empty);
            info.AddValue("StackTraceString", string.Empty);
            info.AddValue("RemoteStackTraceString", string.Empty);
            info.AddValue("RemoteStackIndex", 0);
            info.AddValue("ExceptionMethod", string.Empty);
            info.AddValue("HResult", 1);
            info.AddValue("Source", string.Empty);

            var context = new StreamingContext(StreamingContextStates.File);
            var dataStorageException = new FakeDataStorageException(info, context);

            // Act & Assert
            Assert.IsType(new ArgumentException().GetType(), dataStorageException.InnerException);
        }
    }
}