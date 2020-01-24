using System.Collections.Generic;

namespace GodelTech.Data
{
    /// <summary>
    /// Paged result class.
    /// </summary>
    /// <typeparam name="TItem">The type of the T item.</typeparam>
    public class PagedResult<TItem>
    {
        /// <summary>
        /// Gets or sets page index.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets items count per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets items per page.
        /// </summary>
        public IList<TItem> Items { get; set; }

        /// <summary>
        /// Gets or sets total count of items in repository.
        /// </summary>
        public int TotalCount { get; set; }
    }
}