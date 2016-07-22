using System;
using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;
using CmdCalculator.Interfaces.Parsers;

namespace CmdCalculator.Parsers
{
    public class AllExpressionsParser : IExpressionParser
    {
        private readonly IDictionary<IOperator, IExpressionParser> _operatorParsers;
        private readonly IEnumerable<IOperator> _prioritizedOperators; 

        public AllExpressionsParser(IDictionary<IOperator, IExpressionParser> operatorParsers)
        {
            _operatorParsers = operatorParsers;
            var operators = operatorParsers.Keys;
            operators.ToList().Sort(ComparePriority);
            _prioritizedOperators = operators;
        }

        public bool CanParseExpression(string input)
        {
            return true;
        }

        public IExpression ParseExpression(string input, Func<string, IExpression> innerExpressionParser)
        {
            return ParseExpression(input);
        }

        private IExpression ParseExpression(string input)
        {
            foreach (var op in _prioritizedOperators)
            {
                var operatorParser = _operatorParsers[op];
                if (!operatorParser.CanParseExpression(input))
                {
                    continue;
                }
                var parsedExpression = operatorParser.ParseExpression(input, ParseExpression);
                if (parsedExpression == null)
                {
                    continue;
                }
                return parsedExpression;
            }
            return null;
        }

        private int ComparePriority(IOperator operator1, IOperator operator2)
        {
            return operator1.Priority - operator2.Priority;
        }

    }
}
