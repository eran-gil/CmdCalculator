using System.Collections.Generic;

namespace CmdCalculator.Tokens
{
    class CharTokenParsersProvider : ITokenParsersProvider<char>
    {
        public IEnumerable<ITokenParser<char>> Provide()
        {
            return new ITokenParser<char>[]
            {
                new SpaceTokenParser(),
                new OperatorTokenParser<AdditionToken>("+"), new OperatorTokenParser<SubstractionToken>("-"),
                new OperatorTokenParser<MultiplicationToken>("*"), new OperatorTokenParser<DivisionToken>("/"),
                new OperatorTokenParser<OpenBracketsToken>("("), new OperatorTokenParser<CloseBracketsToken>(")"),
                new NumberTokenParser()
            };
        }
    }
}