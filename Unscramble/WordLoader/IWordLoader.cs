
namespace Unscramble.WordLoader
{
    /// <summary>
    /// Interface for creating an Unscrambler from a file.
    /// </summary>
    public interface IWordLoader
    {
        /// <summary>
        /// Creates an <see cref="IUnscrambler"/> from a file.
        /// </summary>
        /// <param name="Unscrambler">The Unscrambler to load the words into.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Whether or not the words were loaded successfully.</returns>
        public Task<bool> LoadWordsAsync(IUnscrambler Unscrambler, CancellationToken ct = default);
    }
}