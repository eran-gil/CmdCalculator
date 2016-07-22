using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Interfaces.Operators
{
    public interface IUnaryOperator : IOperator
    {
        int GetResult(IExpression operand);
    }
}