using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class DivisionOperator : IOperator
    {
        public string OpRepresentation
        {
            get { return "/"; }
        }
    }
}
