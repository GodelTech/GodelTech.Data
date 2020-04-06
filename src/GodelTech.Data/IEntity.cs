using System.Collections.Generic;

namespace GodelTech.Data
{
    /// <summary>
    /// Interface of entity for data layer.
    /// </summary>
    /// <typeparam name="TType">The type of the T type.</typeparam>
    /// <seealso>
    ///     <cref>System.IEquatable{GodelTech.Data.IEntity{TType}}</cref>
    /// </seealso>
    public interface IEntity<TType> : IEqualityComparer<IEntity<TType>>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        TType Id { get; set; }
    }
}