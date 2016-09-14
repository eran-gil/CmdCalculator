using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Tokenization
{
    public class StringInputReader : IInputReader<char>
    {
        private readonly string _str;
        protected int Position;

        public StringInputReader(string str)
        {
            _str = str;
            Position = 0;
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
            return _str[Position];
        }

        public int Peek(char[] buffer, int numOfCharsToRead)
        {
            var limit = GetReadLimit(numOfCharsToRead);
            for (var i = 0; i < limit; i++)
            {
                buffer[i] = _str[Position + i];
            }
            return limit;
        }

        public char Read()
        {
            var value = Peek();
            if (value != EmptyValue)
            {
                Position++;
            }
            return value;
        }

        public int Read(char[] buffer, int numOfCharsToRead)
        {
            var value = Peek(buffer, numOfCharsToRead);
            Position += value;
            return value;
        }

        private int GetReadLimit(int numOfCharsToRead)
        {
            var availibleChars = _str.Length - Position;
            if (availibleChars < numOfCharsToRead)
            {
                return availibleChars;
            }

            return numOfCharsToRead;
        }
    }
}