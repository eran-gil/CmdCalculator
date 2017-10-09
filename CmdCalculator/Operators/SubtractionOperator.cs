using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class SubtractionOperator : IOperator
    {
        public string OpRepresentation
        {
            get { return "-"; }
        }
    }
}
