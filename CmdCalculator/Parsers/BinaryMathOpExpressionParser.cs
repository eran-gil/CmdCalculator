using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Expressions;
using CmdCalculator.Extensions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;
using CmdCalculator.Tokenization.Tokens;

namespace CmdCalculator.Parsers
{
    public class BinaryMathOpExpressionParser<TOp> : IExpressionParser
        where TOp : IOperator, new()
    {
        private readonly IOperatorToken<TOp> _operatorToken;

        public BinaryMathOpExpressionParser(int priority)
        {
            _operatorToken = new BinaryMathOpToken<TOp>();
            Priority = priority;
        }

        public int Priority { get; private set; }

        public bool CanParseExpression(IEnumerable<IToken> input)
        {
            return input.Contains(_operatorToken);
        }

        public IExpression ParseExpression(ICollection<IToken> input, ITopExpressionParser operandParser)
        {
            var splitLocations = input.GetAllIndexesOf(_operatorToken).ToList();
            splitLocations.Reverse();
            IExpression expression = null;

            foreach (var splitLocation in splitLocations)
            {
                var inputParts = input.SplitAtLocation(splitLocation);
                expression = GetExpressionForParts(inputParts, operandParser);
                if (expression != null)
                {
                    break;
                }
            }

            return expression;
        }

        private IBinaryOpExpression GetExpressionForParts(IEnumerable<IEnumerable<IToken>> splittedInput,
            ITopExpressionParser operandParser)
        {
            var splittedInputArr = splittedInput.ToArray();
            var firstOperand = operandParser.ParseExpression(splittedInputArr[0]);
            var secondOperand = operandParser.ParseExpression(splittedInputArr[1]);

            if (firstOperand == null || secondOperand == null || !IsParsedInCorrectOrder(secondOperand))
            {
                return null;
            }

            var expression = new BinaryOpExpression<TOp>(firstOperand, secondOperand, Priority);
            return expression;
        }

        private bool IsParsedInCorrectOrder(IExpression secondOperand)
        {
            var operatorExpression = secondOperand as IOperatorExpression;
            if (operatorExpression == null)
            {
                return true;
            }
            return operatorExpression.Priority != Priority;
        }


    }
}
