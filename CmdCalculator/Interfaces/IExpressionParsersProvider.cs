using System.Collections.Generic;
using CmdCalculator.Interfaces.Parsers;

namespace CmdCalculator.Interfaces
{
    public interface IExpressionParsersProvider
    {
        IEnumerable<IExpressionParser> Provide();
    }
}