using System;

namespace GodelTech.Data
{
    /// <summary>
    /// Extensions of specification.
    /// </summary>
    public static class SpecificationExtensions
    {
        /// <summary>s
        /// Creates new query parameters for specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the T entity.</typeparam>
        /// <typeparam name="TKey">The type of the T key.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <returns><cref>QueryParameters</cref>.</returns>
        public static QueryParameters<TEntity, TKey> CreateQueryParameters<TEntity, TKey>(
            this Specification<TEntity, TKey> specification)
            where TEntity : class, IEntity<TKey>
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));

            return specification.AsExpression().CreateQueryParameters<TEntity, TKey>();
        }
    }
}
