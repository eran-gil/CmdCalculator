using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class AdditionOperator : IOperator
    {
        public string OpRepresentation
        {
            get { return "+"; }
        }
    }
}
