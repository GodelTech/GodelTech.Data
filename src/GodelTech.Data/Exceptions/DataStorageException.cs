using System;
using System.Runtime.Serialization;

namespace GodelTech.Data
{
    /// <summary>
    /// Is used to indicate that data storage got exception.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class DataStorageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"></see> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"></see>.</param>
        /// <param name="context">The <see cref="StreamingContext"></see>.</param>
        protected DataStorageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStorageException"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public DataStorageException(Exception exception)
            : base(exception.Message, exception)
        {

        }
    }
}