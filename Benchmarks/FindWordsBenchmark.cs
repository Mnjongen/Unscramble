using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using Unscramble.Basic;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class FindWordsBenchmark
    {
        private const string _url = "https://raw.githubusercontent.com/dolph/dictionary/master/unix-words";
        private const string _file = "C:\\Users\\brent\\Documents\\PersonalProjects\\words.txt";
        private const string _letters = "letters";

        private readonly char[] _lettersArray = _letters.ToCharArray();
        private readonly BasicUnscrambler _trie = new();

        public FindWordsBenchmark()
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
            }
        }

        [Benchmark]
        public HashSet<string> Run()
        {
            return _trie.Unscramble(_lettersArray, 6);
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