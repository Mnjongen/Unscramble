using Unscramble;
using Unscramble.Basic;

const string _url = "https://raw.githubusercontent.com/dolph/dictionary/master/unix-words";
const string _file = "C:\\Users\\brent\\Documents\\PersonalProjects\\words.txt";

BasicUnscrambler _trie = new();

var _options = new UnscrambleOptions
{
    MaxLength = 12
};

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

while (true)
{
    var letters = Console.ReadLine();
    if (letters == "exit")
    {
        break;
    }
    var result = _trie.Unscramble(letters.ToArray(), _options);
    Console.WriteLine("\nFound words:");
    foreach (var word in result)
    {
        Console.WriteLine(word);
    }
}