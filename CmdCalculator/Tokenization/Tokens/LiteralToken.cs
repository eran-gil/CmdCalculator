using System.Diagnostics;
using CmdCalculator.Interfaces.Tokens;

namespace CmdCalculator.Tokenization.Tokens
{
    [DebuggerDisplay("{Value}")]
    public class LiteralToken : IToken
    {
        public LiteralToken(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public override bool Equals(object obj)
        {
            var token = obj as LiteralToken;
            if (token != null)
            {
                return token.Value == Value;
            }
            return base.Equals(obj);
        }
    }
}