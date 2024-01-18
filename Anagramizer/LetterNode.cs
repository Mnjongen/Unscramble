using System.Text;

namespace Anagramizer
{
    public class LetterNode
    {
        private readonly Dictionary<char, LetterNode> _letters = [];
        private bool _isWord;

        public LetterNode(bool isWord)
        {
            _isWord = isWord;
        }

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

        public void FindWords(HashSet<string> words, List<char> remainingLetters, int depthRemaining, Stack<char> currentWord)
        {
            if (_isWord)
            {
                words.Add(new string(currentWord.ToArray()));
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
    }
}
