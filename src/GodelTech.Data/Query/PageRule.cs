namespace GodelTech.Data
{
    /// <summary>
    /// Page rule class.
    /// </summary>
    public class PageRule
    {
        /// <summary>
        /// Gets or sets page index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets items count per page.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets a value indicating whether there are valid parameters.
        /// </summary>
        public bool IsValid => Index >= 0 && Size > 0;
    }
}
