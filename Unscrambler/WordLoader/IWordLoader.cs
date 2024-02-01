
namespace Unscrambler.WordLoader
{
    /// <summary>
    /// Interface for creating an unscrambler from a file.
    /// </summary>
    public interface IWordLoader
    {
        /// <summary>
        /// Creates an <see cref="IUnscrambler"/> from a file.
        /// </summary>
        /// <param name="unscrambler">The unscrambler to load the words into.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Whether or not the words were loaded successfully.</returns>
        public Task<bool> LoadWordsAsync(IUnscrambler unscrambler, CancellationToken ct = default);
    }
}