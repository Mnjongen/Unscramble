using System.Text;

namespace Unscramble.Basic;

/// <inheritdoc />
public class BasicUnscrambler : IUnscrambler
{
    private readonly LetterNode _root = new(false);
    /// <inheritdoc />
    public void AddWord(ReadOnlySpan<char> word)
    {
        _root.AddWord(word);
    }
    /// <inheritdoc />
    public void RemoveWord(ReadOnlySpan<char> word)
    {
        _root.RemoveWord(word);
    }
    /// <inheritdoc />
    public HashSet<string> Unscramble(char[] letters, UnscrambleOptions options)
    {
        var words = new HashSet<string>(100);

        _root.FindWords(words, letters.ToList(), options.MaxLength, new StringBuilder(options.MaxLength), options);
        return words;
    }
    /// <inheritdoc />
    public HashSet<string> Unscramble(char[] letters, int maxLength = 32)
    {
        var words = new HashSet<string>(100);

        _root.FindWords(words, letters.ToList(), maxLength, new StringBuilder(maxLength));
        return words;
    }
}