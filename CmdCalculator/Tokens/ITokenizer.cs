namespace CmdCalculator.Tokens
{
    public interface ITokenizer<in T>
    {
        IToken[] Tokenize(T input);
    }
}