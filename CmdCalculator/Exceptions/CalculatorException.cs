using System;

namespace CmdCalculator.Exceptions
{
    public class CalculatorException : Exception
    {


        public CalculatorException(string message) : base(message)
        {
        }
    }
}
