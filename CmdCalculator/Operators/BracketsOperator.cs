using System.Text.RegularExpressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class BracketsOperator : IBracketsOperator
    {
        public Regex OpRegex { get; }

        public int Priority { get; }

        public char OpeningBracket { get; }

        public char ClosingBracket { get; }

        public BracketsOperator(int priority, char openingBracket, char closingBracket)
        {
            Priority = priority;
            var regexStr = string.Format("^\\{0}.+\\{1}$", '(', ')');
            OpRegex = new Regex(regexStr);
            OpeningBracket = openingBracket;
            ClosingBracket = closingBracket;
        }

        public int GetResult(IExpression operand)
        {
            return operand.Evaluate();
        }
    }
}
