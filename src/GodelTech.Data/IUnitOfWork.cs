﻿using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: CLSCompliant(true)]
[assembly: InternalsVisibleTo("GodelTech.Data.Tests")]
namespace GodelTech.Data
{
    /// <summary>
    /// Interface of UnitOfWork for data layer.
    /// </summary>
    /// <seealso cref="IDisposable" />
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
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
