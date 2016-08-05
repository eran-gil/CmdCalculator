using System.Collections.Generic;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Operators;
using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Parsers
{
    public class DefaultExpressionParsersProvider : IExpressionParsersProvider
    {
        public IEnumerable<IExpressionParser> Provide()
        {
            return CreateDefaultParsers();
        }

        private static IEnumerable<IExpressionParser> CreateDefaultParsers()
        {
            var operatorParsers = new List<IExpressionParser>();

            AddBracketOperatorParsers(operatorParsers);
            AddBinaryOperatorParsers(operatorParsers);
            AddLiteralParsers(operatorParsers);
            return operatorParsers;
        }

        private static void AddBracketOperatorParsers(ICollection<IExpressionParser> operatorParsers)
        {
            var parser = new BracketsExpressionParser<OpenBracketsToken<OpeningBracketOperator>, CloseBracketsToken<ClosingBracketOperator>>(4);
            operatorParsers.Add(parser);
        }

        private static void AddBinaryOperatorParsers(ICollection<IExpressionParser> operatorParsers)
        {
            var additionToken = new BinaryMathOpToken<AdditionOperator>(new AdditionOperator());
            IExpressionParser parser = new BinaryMathOpExpressionParser<AdditionOperator>(1, additionToken);
            operatorParsers.Add(parser);

            var subtractionToken = new BinaryMathOpToken<SubtractionOperator>(new SubtractionOperator());
            parser = new BinaryMathOpExpressionParser<SubtractionOperator>(1, subtractionToken);
            operatorParsers.Add(parser);

            var multiplicationToken = new BinaryMathOpToken<MultiplicationOperator>(new MultiplicationOperator());
            parser = new BinaryMathOpExpressionParser<MultiplicationOperator>(2, multiplicationToken);
            operatorParsers.Add(parser);

            var divisionToken = new BinaryMathOpToken<DivisionOperator>(new DivisionOperator());
            parser = new BinaryMathOpExpressionParser<DivisionOperator>(2, divisionToken);
            operatorParsers.Add(parser);
        }

        private static void AddLiteralParsers(ICollection<IExpressionParser> operatorParsers)
        {
            var parser = new LiteralParser(3);
            operatorParsers.Add(parser);
        }
    }
}