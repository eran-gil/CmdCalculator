using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Expressions
{
    public class BinaryOpExpression : IBinaryOpExpression
    {
        public IExpression FirstOperand { get; private set; }

        public IExpression SecondOperand { get; private set; }

        public IBinaryOperator Op { get; private set; }

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
