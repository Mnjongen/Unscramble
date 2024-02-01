using System.Diagnostics;
using Xunit.Abstractions;

namespace Unscrambler.Tests;

[Collection("FindWords")]
public class BasicUnscramblerTests
{
    private readonly ITestOutputHelper _output;
    private readonly WordListFixture _fixture;

    // Load words
    public BasicUnscramblerTests(ITestOutputHelper output, WordListFixture fixture)
    {
        _output = output;
        _fixture = fixture;
    }

    [Theory]
    [InlineData("letters", 7)]
    [InlineData("resources", 10)]
    [InlineData("unscrambler", 12)]
    [InlineData("computer", 10)]
    [InlineData("programming", 6)]
    [InlineData("csharp", 4)]
    [InlineData("dotnet", 2)]
    [InlineData("visualstudio", 5)]
    [InlineData("test", 4)]

    public void CheckForWords(string letters, int maxLength)
    {
        // Find all words using brute force
        var startTime = Stopwatch.GetTimestamp();
        var checkList = _fixture.FindAllWordsUsingBruteForce(letters.ToCharArray(), maxLength);
        var bruteForceTimespan = Stopwatch.GetElapsedTime(startTime);

        // Find all words using the Unscrambler
        startTime = Stopwatch.GetTimestamp();
        var unscrambled = _fixture._unscrambler.Unscramble(letters.ToCharArray(), maxLength);
        var unscrambleTimespan = Stopwatch.GetElapsedTime(startTime);

        // Output how long each method took

        _output.WriteLine($"Brute force took {bruteForceTimespan.TotalMicroseconds} us");
        _output.WriteLine($"Unscramble took {unscrambleTimespan.TotalMicroseconds} us");

        // Output how many times faster unscramble is than brute force
        _output.WriteLine($"Unscramble is {Math.Round(bruteForceTimespan.TotalMicroseconds / unscrambleTimespan.TotalMicroseconds, 3)} times faster than brute force");

        // Check that the unscrambler found all the words
        Assert.Equal(checkList.Count, unscrambled.Count);

        foreach(var word in checkList)
        {
            Assert.Contains(word, unscrambled);
            _output.WriteLine("Found: " + word);
        }
    }
}