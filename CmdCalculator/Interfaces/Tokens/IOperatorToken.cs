using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Interfaces.Tokens
{
    public interface IOperatorToken<out TOp> : IToken
        where TOp : IOperator
    {
        TOp Op { get; }

        string OpRepresentation { get; }
    }
}