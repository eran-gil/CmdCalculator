using System.Collections.Generic;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Tokenization
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