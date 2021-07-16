using System;
using System.Runtime.Serialization;

namespace GodelTech.Data.Tests.Fakes
{
    public class FakeDataStorageExceptionSerializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            if (assemblyName == typeof(DataStorageException).Assembly.FullName && typeName == typeof(DataStorageException).FullName)
            {
                return null;
            }
            else
            {
                throw new ArgumentException("Unexpected type", nameof(typeName));
            }
        }
    }
}