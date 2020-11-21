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
        /// Gets the repository for specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TType">The type of the T type.</typeparam>
        /// <returns>IRepository{TEntity, TType}.</returns>
        IRepository<TEntity, TType> GetRepository<TEntity, TType>()
            where TEntity : class, IEntity<TType>;

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