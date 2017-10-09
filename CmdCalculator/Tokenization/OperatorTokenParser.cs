using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Tokenization
{
    public class OperatorTokenParser<TOp> : ITokenParser<char>
        where TOp : IOperator
    {
        private readonly IOperatorToken<TOp> _operatorToken;

        public OperatorTokenParser(IOperatorToken<TOp> operatorToken)
        {
            _operatorToken = operatorToken;
        }

        public bool CanRead(IInputPeeker<char> peeker)
        {
            var opRepresentation = _operatorToken.OpRepresentation;
            var buffer = new char[opRepresentation.Length];
            var len = peeker.Peek(buffer, buffer.Length);
            return opRepresentation.Equals(new string(buffer, 0, len));
        }

        public IToken ReadToken(IInputReader<char> reader)
        {
            var opRepresentation = _operatorToken.OpRepresentation;
            var buffer = new char[opRepresentation.Length];
            reader.Read(buffer, buffer.Length);
            return _operatorToken;
        }
    }
}