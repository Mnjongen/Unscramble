using Unscrambler.Basic;

namespace Unscrambler.Tests
{
    [CollectionDefinition("FindWords")]
    public class FindWordsCollection : ICollectionFixture<WordListFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    /// <summary>
    /// Use this class to load the word list and unscrambler once for all tests. This will speed up the tests.<br/>
    /// It also moves the code for loading words, from the test file to this fixture.
    /// </summary>
    public class WordListFixture
    {
        // Url to the word list. You can update the word list by changing this url to a different word list, and removing the local file.
        public const string WordListUrl = "https://github.com/dolph/dictionary/raw/master/popular.txt";
        // Path to the word list. This is where the word list will be downloaded to.
        public const string WordListPath = "popular.txt";

        public readonly string[] _words;
        public readonly BasicUnscrambler _unscrambler = new();

        public WordListFixture()
        {
            // Check if the word list exists. If it doesn't, download it.
            if (!File.Exists(WordListPath))
            {
                using var client = new HttpClient();
                var bytes = client.GetByteArrayAsync(WordListUrl).Result;
                File.WriteAllBytes(WordListPath, bytes);
            }

            _words = File.ReadAllLines(WordListPath);

            // Add words to unscrambler
            foreach (var word in _words)
            {
                _unscrambler.AddWord(word);
            }
        }

        /// <summary>
        /// Finds all words using brute force.
        /// </summary>
        public HashSet<string> FindAllWordsUsingBruteForce(char[] letters, int maxLength)
        {
            var words = new HashSet<string>(100);
            foreach (var word in _words)
            {
                if (word.Length > maxLength)
                {
                    continue;
                }
                var lettersCopy = letters.ToList();
                var isMatch = true;
                foreach (var letter in word)
                {
                    if (!lettersCopy.Remove(letter))
                    {
                        isMatch = false;
                        break;
                    }
                }
                if (isMatch)
                {
                    words.Add(word);
                }
            }
            return words;
        }
    }
}
