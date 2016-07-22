using System.Text.RegularExpressions;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class BracketsOperator : IUnaryOperator
    {
        public Regex OpRegex { get; }

        public int Priority { get; }

        public BracketsOperator(int priority)
        {
            Priority = priority;
            OpRegex = new Regex("^\\(.+\\)$");
        }

        public int GetResult(IExpression operand)
        {
            return operand.Evaluate();
        }
    }
}
