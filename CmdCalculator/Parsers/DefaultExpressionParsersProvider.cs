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
            var additionOperator = new AdditionOperator();
            IExpressionParser parser = new BinaryMathOpExpressionParser<AdditionOperator>(1, additionOperator);
            operatorParsers.Add(parser);

            var subtractionOperator = new SubtractionOperator();
            parser = new BinaryMathOpExpressionParser<SubtractionOperator>(1, subtractionOperator);
            operatorParsers.Add(parser);

            var multiplicationOperator = new MultiplicationOperator();
            parser = new BinaryMathOpExpressionParser<MultiplicationOperator>(2, multiplicationOperator);
            operatorParsers.Add(parser);

            var divisionOperator = new DivisionOperator();
            parser = new BinaryMathOpExpressionParser<DivisionOperator>(2, divisionOperator);
            operatorParsers.Add(parser);
        }

        private static void AddLiteralParsers(ICollection<IExpressionParser> operatorParsers)
        {
            var parser = new LiteralParser(3);
            operatorParsers.Add(parser);
        }
    }
}