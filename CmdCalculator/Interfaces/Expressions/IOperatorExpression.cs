namespace CmdCalculator.Interfaces.Expressions
{
    public interface IOperatorExpression : IExpression
    {
        int Priority { get; }
    }
}