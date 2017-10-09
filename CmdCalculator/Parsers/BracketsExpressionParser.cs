using System.Collections.Generic;
using System.Linq;
using CmdCalculator.Expressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Parsers
{
    public class BracketsExpressionParser<TOpen, TClose> : IExpressionParser
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
            var inputList = input as IList<IToken> ?? input.ToList();
            return inputList.Count > 2 && IsWholeExpressionInBrackets(inputList);
        }

        public IExpression ParseExpression(ICollection<IToken> input, ITopExpressionParser operandParser)
        {
            var innerExpressionStr = input.Skip(1).ToList();
            innerExpressionStr.RemoveAt(innerExpressionStr.Count - 1);
            var innerExpression = operandParser.ParseExpression(innerExpressionStr);
            var bracketsExpression = new BracketOpExpression(innerExpression, Priority);
            return bracketsExpression;
        }

        private bool IsWholeExpressionInBrackets(IList<IToken> input)
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
