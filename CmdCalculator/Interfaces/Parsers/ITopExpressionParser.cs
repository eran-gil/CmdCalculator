using System.Collections.Generic;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Interfaces.Parsers
{
    public interface ITopExpressionParser
    {
        IExpression ParseExpression(IEnumerable<IToken> input);
    }
}