namespace CmdCalculator.Interfaces.Tokens
{
    public interface ITokenizerFactory<in T>
    {
        ITokenizer<T> Create();
    }
}