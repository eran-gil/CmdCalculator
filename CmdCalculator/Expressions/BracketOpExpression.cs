using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Expressions
{
    public class BracketOpExpression : IUnaryOpExpression
    {
        public IExpression Operand { get; private set; }
        public int Priority { get; private set; }
        

        public BracketOpExpression(IExpression operand, int priority)
        {
            Operand = operand;
            Priority = priority;
        }

        public override string ToString()
        {
            return "(" + Operand.ToString() + ")";
        }
    }
}
