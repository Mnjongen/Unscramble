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
    /// <inheritdoc />
    public async Task<IUnscrambler?> LoadFromFileAsync(string filePath, CancellationToken ct = default)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }
        try
        {
            var words = File.ReadLinesAsync(filePath, ct);
            var unscrambler = new BasicUnscrambler();
            await foreach (var word in words)
            {
                unscrambler.AddWord(word);
            }
            return unscrambler;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
