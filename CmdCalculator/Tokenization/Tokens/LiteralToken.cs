using System.Diagnostics;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Tokenization.Tokens
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