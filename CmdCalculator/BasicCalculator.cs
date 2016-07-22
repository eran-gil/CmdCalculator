using System.Collections.Generic;
using CmdCalculator.Exceptions;
using CmdCalculator.Interfaces;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Operators;
using CmdCalculator.Parsers;

namespace CmdCalculator
{
    public class BasicCalculator : ICalculator
    {
        private readonly IDictionary<IOperator, IExpressionParser> _operatorParsers;
        private readonly IExpressionParser _expressionParser;


        public BasicCalculator()
        {
            _operatorParsers = new Dictionary<IOperator, IExpressionParser>();

            AddUnaryOperatorParsers();
            AddBinaryOperatorParsers();
            AddLiteralParsers();

            _expressionParser = new AllExpressionsParser(_operatorParsers);
        }

        public BasicCalculator(IDictionary<IOperator, IExpressionParser> operatorParsers, IExpressionParser topParser = null)
        {
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
            var topExpression =_expressionParser.ParseExpression(input, null);
            if (topExpression == null)
            {
                var message = string.Format("The expression \"{0}\" could not be parsed. Please try again.", input);
                throw new CalculatorException(message);
            }
            var result = topExpression.Evaluate();
            return result;
        }

        private void AddUnaryOperatorParsers()
        {
            IUnaryOperator op = new BracketsOperator(4);
            IExpressionParser parser = new BracketsExpressionParser(op);
            _operatorParsers.Add(op, parser);
        }

        private void AddBinaryOperatorParsers()
        {
            IBinaryOperator op = new PlusOperator(1);
            IExpressionParser parser = new BinaryMathOpExpressionParser(op);
            _operatorParsers.Add(op, parser);

            op = new MinusOperator(1);
            parser = new BinaryMathOpExpressionParser(op);
            _operatorParsers.Add(op, parser);

            op = new MultiplyOperator(2);
            parser = new BinaryMathOpExpressionParser(op);
            _operatorParsers.Add(op, parser);

            op = new DivideOperator(2);
            parser = new BinaryMathOpExpressionParser(op);
            _operatorParsers.Add(op, parser);
        }

        private void AddLiteralParsers()
        {
            IOperator op = new LiteralOperator(3);
            IExpressionParser parser = new LiteralParser(op);
            _operatorParsers.Add(op, parser);
        }

    }
}
