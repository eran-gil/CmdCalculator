using System.Diagnostics;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Expressions
{
    public class LiteralExpression : ILiteralExpression
    {

        public LiteralExpression(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return "'" + Value + "'";
        }

        public string Value { get; set; }
    }
}
