using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Interfaces.Expressions
{
    public interface IUnaryOpExpression : IOperatorExpression<IUnaryOperator>
    {
        IExpression Operand { get; }
    }
}
