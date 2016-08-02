namespace CmdCalculator.Tokens
{
    class StringInputReaderFactory : IInputReaderFactory<string, char>
    {
        public IInputReader<char> Create(string input)
        {
            return new StringInputReader(input);
        }
    }
}