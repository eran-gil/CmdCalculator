using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Interfaces.Operators
{
    public interface IBinaryOperator : IOperator
    {
        string OpString { get; }

        int GetResult(IExpression firstOperand, IExpression secondOperand);
    }
}
