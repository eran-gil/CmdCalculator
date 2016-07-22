using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Expressions
{
    public class UnaryOpExpression : IUnaryOpExpression
    {
        public IExpression Operand { get; }

        public IUnaryOperator Op { get; }

        public UnaryOpExpression(IExpression operand, IUnaryOperator op)
        {
            Operand = operand;
            Op = op;
        }

        public int Evaluate()
        {
            return Op.GetResult(Operand);
        }
    }
}
