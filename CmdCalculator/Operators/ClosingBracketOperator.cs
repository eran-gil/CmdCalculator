using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class ClosingBracketOperator : IOperator
    {
        public string OpRepresentation
        {
            get { return ")"; }
        }
    }
}
