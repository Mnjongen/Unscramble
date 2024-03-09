namespace Unscramble
{
    /// <summary>
    /// Options for <see cref="IUnscrambler.Unscramble(char[], UnscrambleOptions)"/>
    /// </summary>
    public class UnscrambleOptions
    {
        /// <summary>
        /// The maximum length of the words to return.
        /// </summary>
        public int MaxLength { get; set; } = 32;
        /// <summary>
        /// The minimum length of the words to return.
        /// </summary>
        public int? MinLength { get; set; }
        /// <summary>
        /// The maximum number of results to return.
        /// </summary>
        public int? MaxResults { get; set; }
        /// <summary>
        /// Words must start with this string.
        /// </summary>
        public string? StartsWith { get; set; }
        /// <summary>
        /// Words must end with this string.
        /// </summary>
        public string? EndsWith { get; set; }
    }
}
