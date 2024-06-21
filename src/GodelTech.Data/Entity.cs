namespace GodelTech.Data
{
    /// <summary>
    /// Entity for data layer.
    /// </summary>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    /// <seealso>
    ///     <cref>GodelTech.Data.IEntity{TKey}</cref>
    /// </seealso>
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual TKey Id { get; set; }
    }
}
