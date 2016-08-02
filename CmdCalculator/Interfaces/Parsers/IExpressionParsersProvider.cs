using System.Collections.Generic;

namespace CmdCalculator.Interfaces.Parsers
{
    public interface IExpressionParsersProvider
    {
        IEnumerable<IExpressionParser> Provide();
    }
}