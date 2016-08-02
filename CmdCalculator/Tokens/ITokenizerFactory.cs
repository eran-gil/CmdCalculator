namespace CmdCalculator.Tokens
{
    public interface ITokenizerFactory<in T>
    {
        ITokenizer<T> Create();
    }
}