using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace GodelTech.Data.Tests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeDataStorageException : DataStorageException
    {
        public FakeDataStorageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public FakeDataStorageException(Exception exception)
            : base(exception)
        {

        }
    }
}