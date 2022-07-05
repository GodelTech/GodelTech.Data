using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace GodelTech.Data.Tests.Exceptions
{
    public class DataStorageExceptionTests
    {
        private static readonly ArgumentNullException InnerException = new ArgumentNullException();

        public static IEnumerable<object[]> ConstructorMemberData =>
            new Collection<object[]>
            {
                new object[] { null, null, null },
                new object[]
                {
                    new DataStorageException(),
                    $"Exception of type '{typeof(DataStorageException)}' was thrown.",
                    null
                },
                new object[]
                {
                    new DataStorageException("Test Message"),
                    "Test Message",
                    null
                },
                new object[]
                {
                    new DataStorageException("Test Message", InnerException),
                    "Test Message",
                    InnerException
                }
            };

        [Theory]
        [MemberData(nameof(ConstructorMemberData))]
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
