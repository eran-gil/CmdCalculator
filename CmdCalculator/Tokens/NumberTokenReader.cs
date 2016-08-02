using System.Text;

namespace CmdCalculator.Tokens
{
    class NumberTokenReader : ITokenReader<char>
    {
        public bool CanRead(IInputPeeker<char> peeker)
        {
            return char.IsDigit(peeker.Peek());
        }

        public IToken ReadToken(IInputReader<char> reader)
        {
            var builder = new StringBuilder();
            while (char.IsDigit(reader.Peek()))
            {
                builder.Append(reader.Read());
            }

            return new NumberToken(builder.ToString());
        }
    }
}