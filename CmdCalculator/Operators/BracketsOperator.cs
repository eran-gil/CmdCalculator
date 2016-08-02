using System.Text.RegularExpressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class BracketsOperator : IBracketsOperator
    {
        public Regex OpRegex { get; private set; }

        public int Priority { get; private set; }

        public char OpeningBracket { get; private set; }

        public char ClosingBracket { get; private set; }

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
