using System.Collections.Generic;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Operators;
using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Tokenization
{
    public class CharTokenParsersProvider : ITokenParsersProvider<char>
    {
        public IEnumerable<ITokenParser<char>> Provide()
        {
            var additionToken = new BinaryMathOpToken<AdditionOperator>(new AdditionOperator());
            var subtractionToken = new BinaryMathOpToken<SubtractionOperator>(new SubtractionOperator());
            var multiplicationToken = new BinaryMathOpToken<MultiplicationOperator>(new MultiplicationOperator());
            var divisionToken = new BinaryMathOpToken<DivisionOperator>(new DivisionOperator());
            var openBracketsToken = new OpenBracketsToken<OpeningBracketOperator>(new OpeningBracketOperator());
            var closingBracketsToken = new CloseBracketsToken<ClosingBracketOperator>(new ClosingBracketOperator());
            return new ITokenParser<char>[]
            {
                new SpaceTokenParser(),
                new OperatorTokenParser<AdditionOperator>(additionToken),
                new OperatorTokenParser<SubtractionOperator>(subtractionToken),
                new OperatorTokenParser<MultiplicationOperator>(multiplicationToken),
                new OperatorTokenParser<DivisionOperator>(divisionToken),
                new OperatorTokenParser<OpeningBracketOperator>(openBracketsToken),
                new OperatorTokenParser<ClosingBracketOperator>(closingBracketsToken),
                new NumberTokenParser()
            };
        }
    }
}