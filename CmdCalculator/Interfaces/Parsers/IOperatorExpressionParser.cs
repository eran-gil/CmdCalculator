using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Interfaces.Parsers
{
    public interface IOperatorExpressionParser <out TOperator> : IExpressionParser
        where TOperator : IOperator
    {
        TOperator Op { get; }
    }
}
