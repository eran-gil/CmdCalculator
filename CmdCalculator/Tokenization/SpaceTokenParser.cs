using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Tokenization
{
    public class SpaceTokenParser : ITokenParser<char>
    {
        public bool CanRead(IInputPeeker<char> peeker)
        {
            return peeker.Peek() == ' ';
        }

        public IToken ReadToken(IInputReader<char> reader)
        {
            while (reader.Peek() == ' ')
            {
                reader.Read();
            }
            return null;
        }
    }
}