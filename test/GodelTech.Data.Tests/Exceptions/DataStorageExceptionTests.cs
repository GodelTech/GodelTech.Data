using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using GodelTech.Data.Tests.Fakes;
using Xunit;

namespace GodelTech.Data.Tests.Exceptions
{
    public class DataStorageExceptionTests
    {
        private static readonly ArgumentNullException InnerException = new ArgumentNullException();

        // https://blog.gurock.com/articles/creating-custom-exceptions-in-dotnet/
        [Fact]
        public async Task Constructor_Protected()
        {
            // Arrange
            var item = new DataStorageException("Test Message");

            byte[] bytes;
            await using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, item);

                bytes = stream.ToArray();
            }

            // Act
            DataStorageException deserializedItem;
            await using (var stream = new MemoryStream(bytes))
            {
                var formatter = new BinaryFormatter
                {
                    Binder = new FakeDataStorageExceptionSerializationBinder()
                };

#pragma warning disable CA2300 // Do not use insecure deserializer BinaryFormatter
                deserializedItem = (DataStorageException) formatter.Deserialize(stream);
#pragma warning restore CA2300 // Do not use insecure deserializer BinaryFormatter
            }

            // Assert
            var exception = await Assert.ThrowsAsync<DataStorageException>(
                () => throw deserializedItem
            );

            Assert.Equal(item.Message, exception.Message);
        }

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