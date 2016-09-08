using System.Collections.Generic;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Parsers;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Parsers
{
    public interface ITopExpressionParser
    {
        IExpression ParseExpression(IEnumerable<IToken> input);
    }
}