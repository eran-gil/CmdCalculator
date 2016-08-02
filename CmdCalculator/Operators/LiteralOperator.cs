using System.Text.RegularExpressions;
using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class LiteralOperator : IOperator
    {
        public Regex OpRegex { get; private set; }

        public int Priority { get; private set; }

        public LiteralOperator(int priority)
        {
            Priority = priority;
            OpRegex = new Regex("^[0-9]+$");
        }
    }
}
