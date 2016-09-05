using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Tokenization
{
    public class StringInputReader : IInputReader<char>
    {
        private readonly string _str;
        private int _count;

        public StringInputReader(string str)
        {
            _str = str;
            _count = 0;
        }

        public char EmptyValue
        {
            get { return '\0'; }
        }

        public char Peek()
        {
            var limit = GetReadLimit(1);
            if (limit == 0)
            {
                return EmptyValue;
            }
            return _str[_count];
        }

        public int Peek(char[] buffer, int numOfCharsToRead)
        {
            var limit = GetReadLimit(numOfCharsToRead);
            for (var i = 0; i < limit; i++)
            {
                buffer[i] = _str[_count + i];
            }
            return limit;
        }

        public char Read()
        {
            var value = Peek();
            if (value != EmptyValue)
            {
                _count++;
            }
            return value;
        }

        public int Read(char[] buffer, int numOfCharsToRead)
        {
            var value = Peek(buffer, numOfCharsToRead);
            _count += value;
            return value;
        }

        private int GetReadLimit(int numOfCharsToRead)
        {
            var availibleChars = _str.Length - _count;
            if (availibleChars < numOfCharsToRead)
            {
                return availibleChars;
            }

            return numOfCharsToRead;
        }
    }
}