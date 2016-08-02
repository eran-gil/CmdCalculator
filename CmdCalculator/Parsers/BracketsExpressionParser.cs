using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Parsers
{
    public class BracketsExpressionParser<TOpen, TClose> : IOperatorExpressionParser
        where TOpen : IToken
        where TClose : IToken
    {
        public BracketsExpressionParser(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; private set; }

        public bool CanParseExpression(IEnumerable<IToken> input)
        {
            return IsWholeExpressionInBrackets(input.ToList());
        }

        public IExpression ParseExpression(IEnumerable<IToken> input, Func<IEnumerable<IToken>, IExpression> operandParser)
        {
            var innerExpressionStr = input.Skip(1).ToList();
            innerExpressionStr.RemoveAt(innerExpressionStr.Count - 1);
            var innerExpression = operandParser(innerExpressionStr);
            var bracketsExpression = new BracketOpExpression(innerExpression, Priority);
            return bracketsExpression;
        }

        private bool IsWholeExpressionInBrackets(List<IToken> input)
        {
            var openBrackets = 0;
            for (var i = 0; i < input.Count; i++)
            {
                var character = input[i];

                openBrackets = GetUpdatedBracketCount(character, openBrackets);

                if (openBrackets == 0 && i < input.Count - 1)
                {
                    return false;
                }
            }

            return openBrackets == 0;
        }

        private int GetUpdatedBracketCount(IToken character, int openBrackets)
        {
            if (character is TOpen)
            {
                openBrackets++;
            }
            else if (character is TClose)
            {
                openBrackets--;
            }
            return openBrackets;
        }
    }
}
