namespace Unscrambler
{
    /// <summary>
    /// A word is a sequence of characters. It is used to build up words without having to allocate a new string each time.
    /// </summary>
    [Obsolete("This class is no longer used.")]
    public class Word
    {
        private readonly char[] _chars;
        private int _size;

        /// <summary>
        /// Create a new word with a maximum size.
        /// </summary>
        /// <param name="size"></param>
        public Word(int size)
        {
            _chars = new char[size];
            _size = 0;
        }

        /// <summary>
        /// Create a new copy of a word.
        /// </summary>
        /// <param name="word"></param>
        public Word(Word word)
        {
            _chars = new char[word._chars.Length];
            _size = word._size;
            Array.Copy(word._chars, _chars, _size);
        }

        /// <summary>
        /// Push a character onto the end of a word.
        /// </summary>
        /// <param name="c"></param>
        public void Push(char c)
        {
            _chars[_size++] = c;
        }
        /// <summary>
        /// Pop a character off the end of a word.
        /// </summary>
        public void Pop()
        {
            _size--;
        }

        /// <summary>
        /// Returns a string representation of the word.
        /// </summary>
        /// <returns>A string representation of the word.</returns>
        public override string ToString()
        {
            return new string(_chars, 0, _size);
        }
    }
}
