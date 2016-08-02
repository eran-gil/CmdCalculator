namespace CmdCalculator.Tokens
{
    public interface ITokenReader<T>
    {
        bool CanRead(IInputPeeker<T> peeker);
        IToken ReadToken(IInputReader<T> reader);
    }
}