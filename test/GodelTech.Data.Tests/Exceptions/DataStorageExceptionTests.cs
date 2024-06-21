using System;
using Xunit;

namespace GodelTech.Data.Tests.Exceptions
{
    public class DataStorageExceptionTests
    {
        private static readonly ArgumentNullException InnerException = new ArgumentNullException();

        public static TheoryData<DataStorageException, string, Exception> ConstructorTestData =>
            new TheoryData<DataStorageException, string, Exception>
            {
                { null, null, null },
                {
                    new DataStorageException(),
                    $"Exception of type '{typeof(DataStorageException)}' was thrown.",
                    null
                },
                {
                    new DataStorageException("Test Message"),
                    "Test Message",
                    null
                },
                {
                    new DataStorageException("Test Message", InnerException),
                    "Test Message",
                    InnerException
                }
            };

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void Constructor(
            DataStorageException item,
            string expectedMessage,
            Exception expectedInnerException)
        {
            // Arrange & Act & Assert
            Assert.Equal(expectedMessage, item?.Message);
            Assert.Equal(expectedInnerException, item?.InnerException);
        }
    }
}
