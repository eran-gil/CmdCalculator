namespace CmdCalculator.Interfaces.Expressions
{
    public interface ILiteralExpression : IExpression
    {
        string Value { get; set; }
    }
}