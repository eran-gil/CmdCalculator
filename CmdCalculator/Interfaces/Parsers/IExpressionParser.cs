using System;
using System.Collections.Generic;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Tokens;

namespace CmdCalculator.Interfaces.Parsers
{
    public interface IExpressionParser
    {
        bool CanParseExpression(IEnumerable<IToken> input);

        IExpression ParseExpression(IEnumerable<IToken> input, Func<IEnumerable<IToken>, IExpression> innerExpressionParser);
        int Priority { get; }
    }
}