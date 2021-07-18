using System;
using System.Collections.Generic;
using System.Linq;

namespace GodelTech.Data
{
    /// <summary>
    /// Paged result class.
    /// </summary>
    /// <typeparam name="TItem">The type of the T item.</typeparam>
    public class PagedResult<TItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{TItem}"/> class.
        /// </summary>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Items count per page.</param>
        /// <param name="items">Items on page.</param>
        /// <param name="totalCount">Total count of items in repository</param>
        public PagedResult(
            int pageIndex,
            int pageSize,
            IEnumerable<TItem> items,
            int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Items = items == null ? new List<TItem>() : items.ToList();
            TotalCount = totalCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{TItem}"/> class.
        /// </summary>
        /// <param name="pageRule">Page rule.</param>
        /// <param name="items">Items on page.</param>
        /// <param name="totalCount">Total count of items in repository</param>
        public PagedResult(
            PageRule pageRule,
            IEnumerable<TItem> items,
            int totalCount)
            : this(
                PassThroughNonNull(pageRule).Index,
                pageRule.Size,
                items,
                totalCount)
        {

        }

        /// <summary>
        /// Gets page index.
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// Gets items count per page.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Gets items on page.
        /// </summary>
        public IReadOnlyCollection<TItem> Items { get; }

        /// <summary>
        /// Gets total count of items in repository.
        /// </summary>
        public int TotalCount { get; }

        private static PageRule PassThroughNonNull(PageRule pageRule)
        {
            if (pageRule == null) throw new ArgumentNullException(nameof(pageRule));

            return pageRule;
        }
    }
}