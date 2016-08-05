using System.Collections.Generic;

namespace CmdCalculator.Interfaces.Tokens
{
    public interface ITokenParsersProvider<T>
    {
        IEnumerable<ITokenParser<T>> Provide();
    }
}