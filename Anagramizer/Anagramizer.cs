namespace Anagramizer
{
    public class Anagramizer
    {
        private readonly LetterNode _root = new(false);
        public void AddWord(ReadOnlySpan<char> word)
        {
            _root.AddWord(word);
        }
        public void RemoveWord(string word)
        {
            _root.RemoveWord(word.AsSpan());
        }
        public HashSet<string> GetWords(char[] letters, int maxLength = 32)
        {
            var words = new HashSet<string>(100);

            _root.FindWords(words, letters.ToList(), maxLength, new Stack<char>(maxLength));
            return words;
        }
    }
}
