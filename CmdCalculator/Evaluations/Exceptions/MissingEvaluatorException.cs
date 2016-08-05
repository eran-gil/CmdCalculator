using System;
using System.Runtime.Serialization;

namespace CmdCalculator.Evaluations.Exceptions
{
    [Serializable]
    public class MissingEvaluatorException : Exception
    {
        public MissingEvaluatorException()
        {
        }

        public MissingEvaluatorException(string message) : base(message)
        {
        }

        public MissingEvaluatorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MissingEvaluatorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}