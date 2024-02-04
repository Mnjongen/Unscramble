---
name: Bug report
about: Report a bug in the Unscrambler project
title: "[BUG]: "
labels: bug
assignees: Mnjongen

---

**Describe the bug**
A clear and concise description of what the bug is.

**Input and output**
What letters did you use as input for the Unscrambler method? What words did you get as output?

**Expected and actual behavior**
What did you expect the Unscrambler method to return? What did it actually return?

**Word list file**
What word list file did you use? Please attach it to the issue or provide a link to it.

**Exception or error message**
Did you encounter any exception or error message? If yes, please copy and paste it here.

**Code snippet**
Please provide a minimal and reproducible code snippet that demonstrates the bug. For example:

```csharp
using Unscrambler;

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

**Environment (please complete the following information):**
 - OS: [e.g. Windows, Linux, MacOS]
 - .NET version: [e.g. 8.0.1]
 - Unscrambler version: [e.g. 1.0.0]

**Additional context**
Add any other context about the problem here.
