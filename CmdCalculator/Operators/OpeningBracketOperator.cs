using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class OpeningBracketOperator : IOperator
    {
        public string OpRepresentation
        {
            get { return "("; }
        }
    }
}
