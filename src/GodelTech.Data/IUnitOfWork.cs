using System;
using System.Threading.Tasks;

namespace GodelTech.Data
{
    /// <summary>
    /// Interface of UnitOfWork for data layer.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits all changes.
        /// </summary>
        /// <returns>Number of rows affected.</returns>
        /// <exception cref="DataStorageException"></exception>
        int Commit();

        /// <summary>
        /// Asynchronously commits all changes.
        /// </summary>
        /// <returns>Number of rows affected.</returns>
        /// <exception cref="DataStorageException"></exception>
        Task<int> CommitAsync();
    }
}