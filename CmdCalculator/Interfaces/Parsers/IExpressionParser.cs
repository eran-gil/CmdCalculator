using System;
using CmdCalculator.Interfaces.Expressions;

namespace CmdCalculator.Interfaces.Parsers
{
    public interface IExpressionParser
    {
        bool CanParseExpression(string input);

        IExpression ParseExpression(string input, Func<string, IExpression> innerExpressionParser);
    }
}