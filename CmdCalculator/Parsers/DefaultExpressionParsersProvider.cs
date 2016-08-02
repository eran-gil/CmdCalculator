using System.Collections.Generic;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Parsers
{
    class DefaultExpressionParsersProvider : IExpressionParsersProvider
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

        private static void AddBracketOperatorParsers(List<IExpressionParser> operatorParsers)
        {
            IOperatorExpressionParser parser = new BracketsExpressionParser<OpenBracketsToken, CloseBracketsToken>(4);
            operatorParsers.Add(parser);
        }

        private static void AddBinaryOperatorParsers(List<IExpressionParser> operatorParsers)
        {
            IOperatorExpressionParser parser = new BinaryMathOpExpressionParser<AdditionToken>(1);
            operatorParsers.Add(parser);

            parser = new BinaryMathOpExpressionParser<SubstractionToken>(1);
            operatorParsers.Add(parser);

            parser = new BinaryMathOpExpressionParser<MultiplicationToken>(2);
            operatorParsers.Add(parser);

            parser = new BinaryMathOpExpressionParser<DivisionToken>(2);
            operatorParsers.Add(parser);
        }

        private static void AddLiteralParsers(List<IExpressionParser> operatorParsers)
        {
            IOperatorExpressionParser parser = new LiteralParser(3);
            operatorParsers.Add(parser);
        }
    }
}