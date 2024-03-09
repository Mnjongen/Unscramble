namespace Unscramble;

/// <summary>
/// Represents an Unscramble. It is used to find all words that can be formed from a given set of letters.
/// </summary>
public interface IUnscrambler
{
    /// <summary>
    /// Adds a word to the dictionary.
    /// </summary>
    /// <param name="word">The word to add to the dictionary.</param>
    void AddWord(ReadOnlySpan<char> word);
    /// <summary>
    /// Retrieves a set of words based on the given letters.
    /// </summary>
    /// <param name="letters">The letters to use when unscrambling.</param>
    /// <param name="options">Extra options for unscrambling.</param>
    /// <returns>A set of words.</returns>
    HashSet<string> Unscramble(char[] letters, UnscrambleOptions options);
    /// <summary>
    /// Retrieves a set of words based on the given letters.
    /// </summary>
    /// <param name="letters">The letters to use when unscrambling.</param>
    /// <param name="maxLength">The maximum length of the words to return.</param>
    /// <returns></returns>
    HashSet<string> Unscramble(char[] letters, int maxLength = 32);
    /// <summary>
    /// Removes a word from the dictionary.
    /// </summary>
    /// <param name="word">The word to remove from the dictionary.</param>
    void RemoveWord(ReadOnlySpan<char> word);
}