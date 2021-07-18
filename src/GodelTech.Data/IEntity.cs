using System.Collections.Generic;

namespace GodelTech.Data
{
    /// <summary>
    /// Interface of entity for data layer.
    /// </summary>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    /// <seealso>
    ///     <cref>GodelTech.Data.IEntity{TKey}</cref>
    /// </seealso>
    public interface IEntity<TKey> : IEqualityComparer<IEntity<TKey>>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        TKey Id { get; set; }
    }
}