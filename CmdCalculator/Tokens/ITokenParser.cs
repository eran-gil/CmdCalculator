namespace CmdCalculator.Tokens
{
    public interface ITokenParser<T>
    {
        bool CanRead(IInputPeeker<T> peeker);
        IToken ReadToken(IInputReader<T> reader);
    }
}