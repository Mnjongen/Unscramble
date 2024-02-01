# Unscrambler

Unscrambler is a .NET package that can help you find all possible words from a list of letters. It is useful for solving word puzzles, crossword clues, or anagrams. Unscrambler is fast, easy to use, and highly optimized.

[![publish](https://github.com/Mnjongen/Unscrambler/actions/workflows/publish.yml/badge.svg?branch=main)](https://github.com/Mnjongen/Unscrambler/actions/workflows/publish.yml)

## Installation

You can install Unscrambler from [NuGet](https://www.nuget.org/packages/Unscrambler/), the package manager for .NET. To install Unscrambler, run the following command in your terminal:

`dotnet add package Unscrambler --version 1.0.0`

## Usage

### Loading words

Before you can use Unscrambler to find words, you need to load the words into the an instance of `IUnscrambler`. You can use any list of words that you want, as long as they are in a text file where each word is on a new line. You can find some examples of word lists online, such as:

- Github repo: [dictionary](https://github.com/dolph/dictionary) by [Dolph Mathews](https://github.com/dolph)
- [Wikipedia](https://en.wiktionary.org/wiki/Wiktionary:Frequency_lists#Frequency_lists) also has some sources
- You can also use your own.

To load the words from a file, you can use the `BasicWordLoader` class that comes with the package. It takes an `IUnscrambler` object and a file path as parameters, and returns a boolean value indicating whether the loading was successful or not.

```csharp
IUnscrambler unscrambler = new BasicUnscrambler();
IWordLoader loader = new BasicWordLoader("words.txt");

// Load words from a file
bool success = await loader.LoadFromFileAsync(unscrambler);

// If success is true, the words are loaded successfully
```

#### Implementing your own word loader

If you are already loading your words from a database or another source, can add the words to a IUnscrambler like this:

```csharp
IUnscrambler unscrambler = new BasicUnscrambler();
foreach (string word in words)
{
    unscrambler.AddWord(word);
}
```

You can also create your own implementation of the `IWordLoader` interface if you want to load words from a different source.

### Finding words

Once you have loaded the words into the package, you can use the `Unscramble` method to find all the words that can be made from a list of letters. The method takes a string of letters as a parameter, and returns a `HashSet<string>` object that contains all the words found.

```csharp
HashSet<string> words = unscrambler.Unscramble("letters");
```

You can then iterate over the `HashSet<string>` object to access the words, or use any other methods that are available for the `HashSet<T>` class.

## Example

Here is an example of how to use Unscrambler to find words from a list of letters and print them to the console:

```csharp
IUnscrambler unscrambler = new BasicUnscrambler();
IWordLoader loader = new BasicWordLoader("words.txt");

// Load words from a file
bool success = await loader.LoadFromFileAsync(unscrambler);

if (success)
{
    // Find words from a list of letters
    HashSet<string> words = unscrambler.Unscramble("letters");

    // Print the words to the console
    foreach (string word in words)
    {
        Console.WriteLine(word);
    }
}
else
{
    Console.WriteLine("Failed to load words from file.");
}
```
