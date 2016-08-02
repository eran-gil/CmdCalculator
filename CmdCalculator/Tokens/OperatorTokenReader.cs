namespace CmdCalculator.Tokens
{
    class OperatorTokenReader<TOperator> : ITokenReader<char>
        where TOperator : IToken, new()
    {
        private string _operator;

        public OperatorTokenReader(string op)
        {
            _operator = op;
        }
        public bool CanRead(IInputPeeker<char> peeker)
        {
            var buffer = new char[_operator.Length];
            var len = peeker.Peek(buffer, buffer.Length);
            return _operator.Equals(new string(buffer, 0, len));
        }

        public IToken ReadToken(IInputReader<char> reader)
        {
            var buffer = new char[_operator.Length];
            reader.Read(buffer, buffer.Length);
            return new TOperator();
        }
    }
}