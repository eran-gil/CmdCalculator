using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Interfaces.Expressions
{
    public interface IOperatorExpression<out TOperator> : IExpression 
        where TOperator : IOperator
    {
        TOperator Op { get; }
    }
}