namespace CmdCalculator.Interfaces.Expressions
{
    public interface IBinaryOpExpression : IOperatorExpression
    {
        IExpression FirstOperand { get; }

        IExpression SecondOperand { get; }
    }
}
