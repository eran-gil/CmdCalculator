using System.Diagnostics;

namespace CmdCalculator.Tokens
{
    [DebuggerDisplay("{Value}")]
    class LiteralToken : IToken
    {
        public LiteralToken(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}