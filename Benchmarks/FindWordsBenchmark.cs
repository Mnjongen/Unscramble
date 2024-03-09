using BenchmarkDotNet.Attributes;
using Unscramble;
using Unscramble.Basic;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class FindWordsBenchmark
    {
        private const string _url = "https://raw.githubusercontent.com/dolph/dictionary/master/unix-words";
        private const string _file = "C:\\Users\\brent\\Documents\\PersonalProjects\\words.txt";
        private const string _letters = "lettersabcd";

        private readonly char[] _lettersArray = _letters.ToCharArray();
        private readonly BasicUnscrambler _trie = new();
        private readonly BasicUnscramblerNew _trieNew = new();

        private UnscrambleOptions _options = null!;

        [Params(10)]
        public int _maxLength;

        [GlobalSetup]
        public void Setup()
        {
            if (!File.Exists(_file))
            {
                using var client = new HttpClient();
                using var stream = client.GetStreamAsync(_url).Result;
                using var reader = new StreamReader(stream);

                // store the words in the file
                using var writer = new StreamWriter(_file);
                while (!reader.EndOfStream)
                {
                    var word = reader.ReadLine();
                    if (word == null)
                    {
                        continue;
                    }
                    writer.WriteLine(word);
                }
            }

            // Read the words from the file line by line
            using var fileStream = File.OpenRead(_file);
            using var fileReader = new StreamReader(fileStream);
            while (!fileReader.EndOfStream)
            {
                var word = fileReader.ReadLine();
                if (word == null)
                {
                    continue;
                }
                _trie.AddWord(word);
                _trieNew.AddWord(word);
            }

            _options = new UnscrambleOptions
            {
                MaxLength = _maxLength
            };
        }

        [Benchmark(Baseline = true)]
        public HashSet<string> RunNormal()
        {
            return _trie.Unscramble(_lettersArray, _maxLength);
        }
        [Benchmark]
        public HashSet<string> RunNew()
        {
            return _trieNew.Unscramble(_lettersArray, _maxLength);
        }
    }
}
// Start

//| Method  | Mean      | Error     | StdDev    | Allocated  |
//| ------- | ---------:| ---------:| ---------:| ----------:|
//| Run     | 13.03 us  | 0.191 us  | 0.170 us  | 13.71 KB   |


// Initialize result hashset with capacity of 100

//| Method  | Mean      | Error     | StdDev    | Allocated  |
//| ------- | ---------:| ---------:| ---------:| ----------:|
//| Run     | 12.17 us  | 0.191 us  | 0.169 us  | 12.61 KB   |


// Changed current word from a string to a list of chars

//| Method  | Mean      | Error     | StdDev    | Allocated  |
//| ------- | ---------:| ---------:| ---------:| ----------:|
//| Run     | 11.68 us  | 0.152 us  | 0.142 us  | 7.93 KB    |


// Initialize word list with capacity of max word length

//| Method  | Mean      | Error     | StdDev    | Allocated  |
//| ------- | ---------:| ---------:| ---------:| ----------:|
//| Run     | 11.30 us  | 0.217 us  | 0.192 us  | 7.9 KB     |


// Changed current word from a list of chars to a StringBuilder

//| Method  | Mean      | Error     | StdDev    | Allocated  |
//| ------- | ---------:| ---------:| ---------:| ----------:|
//| Run     | 11.81 us  | 0.201 us  | 0.188 us  | 5.03 KB    |


// Changed current word from a StringBuilder to a Stack<char>
// WARNING INVALID: Converting to string wasn't properly implemented

//| Method  | Mean      | Error     | StdDev    | Allocated  |
//| ------- | ---------:| ---------:| ---------:| ----------:|
//| Run     | 11.35 us  | 0.201 us  | 0.231 us  | 2.34 KB    |


// Changed current word from a Stack<char> to a custom Word class

//| Method  | Mean      | Error     | StdDev    | Allocated  |
//| ------- | ---------:| ---------:| ---------:| ----------:|
//| Run     | 10.76 us  | 0.159 us  | 0.141 us  | 5.02 KB    |



//| Method               | Mean     | Error    | StdDev   | Gen0   | Allocated  |
//| -------------------- | --------:| --------:| --------:| ------:| -------- -:|
//| Run                  | 10.99 us | 0.142 us | 0.133 us | 0.3967 | 5.02 KB    |
//| RunOptimized         | 10.75 us | 0.208 us | 0.317 us | 0.3967 | 5.02 KB    |
//| RunWithStringBuilder | 10.67 us | 0.212 us | 0.226 us | 0.3967 | 5.02 KB    | This is the one that will be used going forward


// Changed from a dictionary to a list of tuples (No data, about 20% improvement)


// Changed from foreach to for loop (and increased the input letters)

//| Method     | Mean      | Error    | StdDev    | Median    | Gen0    | Gen1    | Allocated  |
//| ---------- | ---------:| --------:| ---------:| ---------:| -------:| -------:| ----------:|
//| RunNormal  | 223.6 us  | 4.45 us  | 12.69 us  | 219.6 us  | 5.3711  | 0.7324  | 65.98 KB   |
//| RunNew     | 201.3 us  | 4.00 us  | 11.15 us  | 202.2 us  | 5.3711  | 0.7324  | 65.98 KB   |

