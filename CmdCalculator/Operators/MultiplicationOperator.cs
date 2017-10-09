using CmdCalculator.Interfaces.Operators;

namespace CmdCalculator.Operators
{
    public class MultiplicationOperator : IOperator
    {
        public string OpRepresentation
        {
            get { return "*"; }
        }
    }
}
