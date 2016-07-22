namespace CmdCalculator.Interfaces.Operators
{
    public interface IBracketsOperator : IUnaryOperator
    {
        char OpeningBracket { get; }

        char ClosingBracket { get; }
    }
}