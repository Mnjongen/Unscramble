using System.Text;

namespace Unscrambler
{
    /// <summary>
    /// Represents a letter node in the trie.
    /// </summary>
    public class LetterNode
    {
        /// <summary>
        /// All letters that can follow this letter. If the letter is not in this dictionary, then there are no words that can be formed from this letter.
        /// </summary>
        private readonly Dictionary<char, LetterNode> _letters = [];
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
            if (!_letters.TryGetValue(nextLetter, out var child))
            {
                child = new LetterNode(false);
                _letters.Add(nextLetter, child);
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
            if (!_letters.TryGetValue(nextLetter, out var child))
            {
                return;
            }

            child.RemoveWord(lettersRemaining[1..]);
        }
        /// <summary>
        /// Checks if this node is the end of a word, and if so, adds it to the set of words.<br/>
        /// It then calls all of its children to find more words.
        /// </summary>
        /// <param name="words">The current list of words.</param>
        /// <param name="remainingLetters">The remaining letters that can be used.</param>
        /// <param name="depthRemaining">The depth remaining.</param>
        /// <param name="currentWord">Represents the letters used to get to this node</param>
        [Obsolete("This method is no longer used.")]
        public void FindWords(HashSet<string> words, List<char> remainingLetters, int depthRemaining, Word currentWord)
        {
            if (_isWord)
            {
                words.Add(currentWord.ToString());
            }

            if (depthRemaining == 0)
            {
                return;
            }

            foreach (var letter in _letters)
            {
                if (remainingLetters.Contains(letter.Key))
                {
                    remainingLetters.Remove(letter.Key);
                    currentWord.Push(letter.Key);
                    letter.Value.FindWords(words, remainingLetters, depthRemaining - 1, currentWord);
                    currentWord.Pop();
                    remainingLetters.Add(letter.Key);
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

            foreach (var letter in _letters)
            {
                if (remainingLetters.Contains(letter.Key))
                {
                    remainingLetters.Remove(letter.Key);
                    currentWord.Append(letter.Key);
                    letter.Value.FindWords(words, remainingLetters, depthRemaining - 1, currentWord);
                    currentWord.Remove(currentWord.Length - 1, 1);
                    remainingLetters.Add(letter.Key);
                }
            }
        }
    }
}
