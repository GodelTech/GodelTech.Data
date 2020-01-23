using System;

// ReSharper disable once CheckNamespace
namespace GodelTech.Data
{
    /// <summary>
    /// Is used to indicate that data storage got exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class DataStorageException : Exception
    {
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