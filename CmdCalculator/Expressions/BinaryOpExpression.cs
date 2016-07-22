using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Expressions
{
    public class BinaryOpExpression : IBinaryOpExpression
    {
        public IExpression FirstOperand { get; }

        public IExpression SecondOperand { get; }

        public IBinaryOperator Op { get; }

        public BinaryOpExpression(IExpression firstOperand, IExpression secondOperand, IBinaryOperator op)
        {
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Op = op;
        }

        public int Evaluate()
        {
            return Op.GetResult(FirstOperand, SecondOperand);
        }
    }
}
