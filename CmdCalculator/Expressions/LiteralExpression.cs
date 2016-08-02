using System.Diagnostics;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Expressions
{
    public class LiteralExpression : IExpression
    {
        private readonly string _value;

        public LiteralExpression(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return "'" + _value + "'";
        }
    }
}
