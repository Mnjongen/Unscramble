# Unscramble

Unscramble is a .NET package that can help you find all possible words from a list of letters. It is useful for solving word puzzles, crossword clues, or anagrams. Unscramble is fast, easy to use, and highly optimized.

[![publish](https://github.com/Mnjongen/Unscramble/actions/workflows/publish.yml/badge.svg?branch=main)](https://github.com/Mnjongen/Unscramble/actions/workflows/publish.yml)
[![NuGet Version](https://img.shields.io/nuget/v/Unscramble)](https://www.nuget.org/packages/Unscramble/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Unscramble)](https://www.nuget.org/packages/Unscramble/)



## Installation

You can install Unscramble from [NuGet](https://www.nuget.org/packages/Unscramble/), the package manager for .NET. To install Unscramble, run the following command in your terminal:

`dotnet add package Unscramble`

## Usage

### Loading words

Before you can use Unscramble to find words, you need to load the words into the an instance of `IUnscramble`. You can use any list of words that you want, as long as they are in a text file where each word is on a new line. You can find some examples of word lists online, such as:

- Github repo: [dictionary](https://github.com/dolph/dictionary) by [Dolph Mathews](https://github.com/dolph)
- [Wikipedia](https://en.wiktionary.org/wiki/Wiktionary:Frequency_lists#Frequency_lists) also has some sources
- You can also use your own.

To load the words from a file, you can use the `BasicWordLoader` class that comes with the package. It takes an `IUnscramble` object and a file path as parameters, and returns a boolean value indicating whether the loading was successful or not.

```csharp
IUnscramble Unscramble = new BasicUnscramble();
IWordLoader loader = new BasicWordLoader("words.txt");

// Load words from a file
bool success = await loader.LoadFromFileAsync(Unscramble);

// If success is true, the words are loaded successfully
```

#### Implementing your own word loader

If you are already loading your words from a database or another source, can add the words to a IUnscramble like this:

```csharp
IUnscramble Unscramble = new BasicUnscramble();
foreach (string word in words)
{
    Unscramble.AddWord(word);
}
```

You can also create your own implementation of the `IWordLoader` interface if you want to load words from a different source.

### Finding words

Once you have loaded the words into the package, you can use the `Unscramble` method to find all the words that can be made from a list of letters. The method takes a string of letters as a parameter, and returns a `HashSet<string>` object that contains all the words found.

```csharp
HashSet<string> words = Unscramble.Unscramble("letters");
```

You can then iterate over the `HashSet<string>` object to access the words, or use any other methods that are available for the `HashSet<T>` class.

## Example

Here is a full example of how to use Unscramble to find words from a list of letters and print them to the console:

```csharp
IUnscramble Unscramble = new BasicUnscramble();
IWordLoader loader = new BasicWordLoader("words.txt");

// Load words from a file
bool success = await loader.LoadFromFileAsync(Unscramble);

if (success)
{
    // Find words from a list of letters
    HashSet<string> words = Unscramble.Unscramble("letters");

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
