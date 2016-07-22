using System.Text.RegularExpressions;
using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class LiteralOperator : IOperator
    {
        public Regex OpRegex { get; }

        public int Priority { get; }

        public LiteralOperator(int priority)
        {
            Priority = priority;
            OpRegex = new Regex("^[0-9]+$");
        }
    }
}
