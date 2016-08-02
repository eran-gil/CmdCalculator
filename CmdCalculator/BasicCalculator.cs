using System;
using System.Collections.Generic;
using CmdCalculator.Exceptions;
using CmdCalculator.Interfaces;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Parsers;
using CmdCalculator.Tokens;

namespace CmdCalculator
{
    public class BasicCalculator : ICalculator
    {
        private List<IExpressionParser> _operatorParsers;
        private readonly IExpressionParser _expressionParser;
        private StringTokenizer _inputTokenizer;

        public BasicCalculator()
            : this(CreateDefaultParsers())
        {
        }

        

        public BasicCalculator(List<IExpressionParser> operatorParsers, IExpressionParser topParser = null)
        {
            _inputTokenizer = new StringTokenizer(new SpaceTokenReader(),
                new OperatorTokenReader<AdditionToken>("+"), new OperatorTokenReader<SubstractionToken>("-"),
                new OperatorTokenReader<MultiplicationToken>("*"), new OperatorTokenReader<DivisionToken>("/"),
                new OperatorTokenReader<OpenBracketsToken>("("), new OperatorTokenReader<CloseBracketsToken>(")"),
                new NumberTokenReader());
            _operatorParsers = operatorParsers;

            if (topParser == null)
            {
                _expressionParser = new AllExpressionsParser(_operatorParsers);
            }
            else
            {
                _expressionParser = topParser;
            }
        }

        public int Calculate(string input)
        {
            var tokenizedInput = _inputTokenizer.Tokenize(input);
            var topExpression = _expressionParser.ParseExpression(tokenizedInput, null);
            if (topExpression == null)
            {
                var message = string.Format("The expression \"{0}\" could not be parsed. Please try again.", input);
                throw new CalculatorException(message);
            }

            throw new NotImplementedException();
            //var result = topExpression.Evaluate();
            //return result;
        }

        private static List<IExpressionParser> CreateDefaultParsers()
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
