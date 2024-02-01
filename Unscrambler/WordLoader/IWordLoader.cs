
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
        /// <param name="filePath">The path to the file.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>An <see cref="IUnscrambler"/> if the file exists and can be read, otherwise null.</returns>
        public Task<bool> LoadFromFileAsync(IUnscrambler unscrambler, string filePath, CancellationToken ct = default);
    }
}