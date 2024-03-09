using System.Text;

namespace Unscramble.Basic;

/// <summary>
/// Represents a letter node in the trie.
/// </summary>
public class LetterNode
{
    /// <summary>
    /// All letters that can follow this letter. If the letter is not in this dictionary, then there are no words that can be formed from this letter.
    /// </summary>
    private readonly List<(char Letter, LetterNode Node)> _letters = [];
    private bool _isWord;

    /// <summary>
    /// Creates a new instance of <see cref="LetterNode"/>.
    /// </summary>
    /// <param name="isWord">Whether or not this node represents a word.</param>
    public LetterNode(bool isWord)
    {
        _isWord = isWord;
    }

    /// <summary>
    /// Process the letters remaining in the word. It adds nodes where necessary.
    /// </summary>
    /// <param name="remainingLetters">The letters remaining in the word.</param>
    public void AddWord(ReadOnlySpan<char> remainingLetters)
    {
        if (remainingLetters.Length == 0)
        {
            _isWord = true;
            return;
        }

        var nextLetter = remainingLetters[0];

        var child = _letters.FirstOrDefault(x => x.Letter == nextLetter).Node;
        if (child == null)
        {
            child = new LetterNode(false);
            _letters.Add((nextLetter, child));
        }

        child.AddWord(remainingLetters[1..]);
    }

    /// <summary>
    /// Removes the word from the trie. It removes nodes when they are no longer used.
    /// </summary>
    /// <param name="lettersRemaining"></param>
    public void RemoveWord(ReadOnlySpan<char> lettersRemaining)
    {
        if (lettersRemaining.Length == 0)
        {
            _isWord = false;
            return;
        }

        var nextLetter = lettersRemaining[0];

        var child = _letters.FirstOrDefault(x => x.Letter == nextLetter).Node;

        if (child == null)
        {
            return;
        }

        child.RemoveWord(lettersRemaining[1..]);

        if (child._letters.Count == 0 && !child._isWord)
        {
            _letters.Remove((nextLetter, child));
        }
    }
    /// <summary>
    /// Checks if this node is the end of a word, and if so, adds it to the set of words.<br/>
    /// It then calls all of its children to find more words.
    /// </summary>
    /// <param name="words">The current list of words.</param>
    /// <param name="remainingLetters">The remaining letters that can be used.</param>
    /// <param name="depthRemaining">The depth remaining.</param>
    /// <param name="currentWord">Represents the letters used to get to this node</param>
    public void FindWords(HashSet<string> words, List<char> remainingLetters, int depthRemaining, StringBuilder currentWord)
    {
        if (_isWord)
        {
            words.Add(currentWord.ToString());
        }

        if (depthRemaining == 0)
        {
            return;
        }

        foreach (var (letter, node) in _letters)
        {
            if (remainingLetters.Contains(letter))
            {
                remainingLetters.Remove(letter);
                currentWord.Append(letter);
                node.FindWords(words, remainingLetters, depthRemaining - 1, currentWord);
                currentWord.Remove(currentWord.Length - 1, 1);
                remainingLetters.Add(letter);
            }
        }
    }
    /// <summary>
    /// Checks if this node is the end of a word, and if so, adds it to the set of words.<br/>
    /// It then calls all of its children to find more words.
    /// </summary>
    /// <param name="words">The current list of words.</param>
    /// <param name="remainingLetters">The remaining letters that can be used.</param>
    /// <param name="depthRemaining">The depth remaining.</param>
    /// <param name="currentWord">Represents the letters used to get to this node</param>
    /// <param name="options">The options to use when unscrambling.</param>
    public void FindWords(HashSet<string> words, List<char> remainingLetters, int depthRemaining, StringBuilder currentWord, UnscrambleOptions options)
    {
        // If this node is a word, check if it is a valid word and add it to the list of words
        if (_isWord)
        {
            // Convert the StringBuilder to a string
            string currentWordString = currentWord.ToString();

            // Validate the word
            if (ValidateWord(currentWordString, options))
            {
                // Add the valid word to the list of words
                words.Add(currentWordString);

                // Return early if we have reached the maximum number of results
                if (options.MaxResults != null && words.Count >= options.MaxResults)
                    return;
            }
        }

        // Return early if we have reached the maximum depth
        if (depthRemaining == 0)
        {
            return;
        }

        // Use the StartsWith to filter out words that don't start with the given string
        if (options.StartsWith != null && currentWord.Length < options.StartsWith.Length)
        {
            var letter = options.StartsWith[currentWord.Length];

            // Check if there is a letter that matches the next letter in the StartsWith string
            // Skip the rest of the code if there isn't, as it must start with the StartsWith string
            if (!remainingLetters.Contains(letter))
                return;

            // Only process the next letter in the StartsWith string
            remainingLetters.Remove(letter);
            currentWord.Append(letter);
            FindWords(words, remainingLetters, depthRemaining - 1, currentWord, options);
            currentWord.Remove(currentWord.Length - 1, 1);
            remainingLetters.Add(letter);

            // Return early, as we don't want to process the rest of the letters
            // Only letters in the StartsWith string are allowed
            return;
        }

        // Loop through all the letters that can be used
        for (int i = 0; i < _letters.Count; i++)
        {
            // Check if the letter can be used
            if (remainingLetters.Remove(_letters[i].Letter))
            {
                currentWord.Append(_letters[i].Letter);
                _letters[i].Node.FindWords(words, remainingLetters, depthRemaining - 1, currentWord);
                currentWord.Remove(currentWord.Length - 1, 1);
                remainingLetters.Add(_letters[i].Letter);
            }
        }
    }
    private static bool ValidateWord(string word, UnscrambleOptions options)
    {
        if (options.MinLength != null && word.Length < options.MinLength)
            return false;

        if (word.Length > options.MaxLength)
            return false;

        if (options.StartsWith != null && !word.StartsWith(options.StartsWith))
            return false;

        if (options.EndsWith != null && !word.EndsWith(options.EndsWith))
            return false;

        return true;
    }
}
