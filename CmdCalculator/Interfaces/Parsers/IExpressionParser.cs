using System;
using System.Collections.Generic;
using CmdCalculator.Interfaces.Expressions;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Interfaces.Parsers
{
    public interface IExpressionParser
    {
        bool CanParseExpression(IEnumerable<IToken> input);

        int Priority { get; }

        IExpression ParseExpression(IEnumerable<IToken> input, Func<IEnumerable<IToken>, IExpression> innerExpressionParser);
    }
}