using System;

namespace GodelTech.Data.Specification
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
            this ISpecification<TEntity, TKey> specification)
            where TEntity : class, IEntity<TKey>
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));

            return new QueryParameters<TEntity, TKey>
            {
                Filter = new FilterRule<TEntity, TKey>(specification)
            };
        }
    }
}
