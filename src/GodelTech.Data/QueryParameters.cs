namespace GodelTech.Data
{
    /// <summary>
    /// Query parameters class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    public class QueryParameters<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Gets or sets filter rule.
        /// </summary>
        public FilterRule<TEntity, TKey> Filter { get; set; }

        /// <summary>
        /// Gets or sets sort rule.
        /// </summary>
        public SortRule<TEntity, TKey> Sort { get; set; }

        /// <summary>
        /// Gets or sets page rule.
        /// </summary>
        public PageRule Page { get; set; }
    }
}