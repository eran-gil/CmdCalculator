namespace CmdCalculator.Tokens
{
    class SpaceTokenReader : ITokenReader<char>
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