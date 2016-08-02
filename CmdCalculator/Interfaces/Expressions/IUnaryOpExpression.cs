namespace CmdCalculator.Interfaces.Expressions
{
    public interface IUnaryOpExpression : IOperatorExpression
    {
        IExpression Operand { get; }
    }
}
