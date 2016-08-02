using System.Collections.Generic;

namespace CmdCalculator.Tokens
{
    public interface ITokenParsersProvider<T>
    {
        IEnumerable<ITokenParser<T>> Provide();
    }
}