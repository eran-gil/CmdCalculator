using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Expressions
{
    public class BinaryOpExpression<T> : IBinaryOpExpression
    {
        public IExpression FirstOperand { get; private set; }
        public IExpression SecondOperand { get; private set; }
        public int Priority { get; private set; }

        public BinaryOpExpression(IExpression firstOperand, IExpression secondOperand, int priority)
        {
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Priority = priority;
        }

        public override string ToString()
        {
            return string.Format("{0}({1},{2})",typeof (T).Name,FirstOperand,SecondOperand);
        }
    }
}
