using System.Text.RegularExpressions;

namespace CmdCalculator.Interfaces.Operators
{
    public interface IOperator
    {
        Regex OpRegex { get; }

        int Priority { get; }
    }
}
