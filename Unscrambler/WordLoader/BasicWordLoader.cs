using Unscrambler.Basic;

namespace Unscrambler.WordLoader;

/// <summary>
/// Loads words from a file into a <see cref="BasicUnscrambler"/>.
/// </summary>
/// <remarks>
/// Words are expected to be separated by newlines.
/// </remarks>
public class BasicWordLoader : IWordLoader
{
    private readonly string _filePath;

    /// <summary>
    /// Creates a new instance of <see cref="BasicWordLoader"/>.
    /// </summary>
    /// <param name="filePath">The path to the file containing the words.</param>
    public BasicWordLoader(string filePath)
    {
        _filePath = filePath;
    }

    /// <inheritdoc />
    public async Task<bool> LoadWordsAsync(IUnscrambler unscrambler, CancellationToken ct = default)
    {
        if (!File.Exists(_filePath))
        {
            return false;
        }
        try
        {
            var words = File.ReadLinesAsync(_filePath, ct);
            await foreach (var word in words)
            {
                unscrambler.AddWord(word);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
