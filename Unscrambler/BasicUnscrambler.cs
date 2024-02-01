using System.Text;

namespace Unscrambler
{
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
        public void RemoveWord(string word)
        {
            _root.RemoveWord(word.AsSpan());
        }
        /// <inheritdoc />
        public HashSet<string> Unscramble(char[] letters, int maxLength = 32)
        {
            var words = new HashSet<string>(100);

            _root.FindWords(words, letters.ToList(), maxLength, new StringBuilder(maxLength));
            return words;
        }
    }
}