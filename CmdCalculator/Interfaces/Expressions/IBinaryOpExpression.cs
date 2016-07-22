using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Interfaces.Expressions
{
    public interface IBinaryOpExpression : IOperatorExpression<IBinaryOperator>
    {
        IExpression FirstOperand { get; }

        IExpression SecondOperand { get; }
    }
}
