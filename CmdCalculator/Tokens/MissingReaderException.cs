using System;
using System.Runtime.Serialization;

namespace CmdCalculator.Tokens
{
    [Serializable]
    public class MissingReaderException : Exception
    {
        public MissingReaderException()
        {
        }

        public MissingReaderException(string message) : base(message)
        {
        }

        public MissingReaderException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MissingReaderException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}