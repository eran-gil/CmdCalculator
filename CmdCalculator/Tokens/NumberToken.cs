using System.Diagnostics;

namespace CmdCalculator.Tokens
{
    [DebuggerDisplay("{Value}")]
    class NumberToken : IToken
    {
        public NumberToken(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}