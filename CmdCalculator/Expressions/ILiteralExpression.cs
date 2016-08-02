using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Expressions
{
    public interface ILiteralExpression : IExpression
    {
        string Value { get; set; }
    }
}