using System.Collections.Generic;

namespace CmdCalculator.Interfaces.Tokens
{
    public interface ITokenizer<in T>
    {
        IEnumerable<IToken> Tokenize(T input);
    }
}