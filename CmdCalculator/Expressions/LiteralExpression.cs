using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Expressions
{
    public class LiteralExpression : IExpression
    {
        private readonly int _value;
        public LiteralExpression(int value)
        {
            _value = value;
        }


        public int Evaluate()
        {
            return _value;
        }
    }
}
