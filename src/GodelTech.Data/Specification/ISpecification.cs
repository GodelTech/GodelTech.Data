namespace GodelTech.Data.Specification
{
    /// <summary>
    /// Interface of specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of the T entity.</typeparam>
    /// <typeparam name="TKey">The type of the T key.</typeparam>
    public interface ISpecification<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// Checks if candidate satisfies.
        /// </summary>
        /// <param name="candidate">Candidate.</param>
        /// <returns>Boolean result of check.</returns>
        bool IsSatisfiedBy(TEntity candidate);

        /// <summary>
        /// And.
        /// </summary>
        /// <param name="other">Other candidate.</param>
        /// <returns>Specification.</returns>
#pragma warning disable CA1716 // Identifiers should not match keywords
        // You can suppress a warning from this rule if you're sure that the identifier won't confuse users of the API, and that the library is usable in all available languages in .NET.
        ISpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> other);
#pragma warning restore CA1716 // Identifiers should not match keywords

        /// <summary>
        /// And not.
        /// </summary>
        /// <param name="other">Other candidate.</param>
        /// <returns>Specification.</returns>
        ISpecification<TEntity, TKey> AndNot(ISpecification<TEntity, TKey> other);

        /// <summary>
        /// Or.
        /// </summary>
        /// <param name="other">Other candidate.</param>
        /// <returns>Specification.</returns>
#pragma warning disable CA1716 // Identifiers should not match keywords
        // You can suppress a warning from this rule if you're sure that the identifier won't confuse users of the API, and that the library is usable in all available languages in .NET.
        ISpecification<TEntity, TKey> Or(ISpecification<TEntity, TKey> other);
#pragma warning restore CA1716 // Identifiers should not match keywords

        /// <summary>
        /// Or not.
        /// </summary>
        /// <param name="other">Other candidate.</param>
        /// <returns>Specification.</returns>
        ISpecification<TEntity, TKey> OrNot(ISpecification<TEntity, TKey> other);

        /// <summary>
        /// Not.
        /// </summary>
        /// <returns>Specification.</returns>
#pragma warning disable CA1716 // Identifiers should not match keywords
        // You can suppress a warning from this rule if you're sure that the identifier won't confuse users of the API, and that the library is usable in all available languages in .NET.
        ISpecification<TEntity, TKey> Not();
#pragma warning restore CA1716 // Identifiers should not match keywords
    }
}
