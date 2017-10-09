using System.Text;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Tokenization
{
    public class NumberTokenParser : ITokenParser<char>
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

            return new LiteralToken(builder.ToString());
        }
    }
}