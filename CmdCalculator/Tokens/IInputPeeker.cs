namespace CmdCalculator.Tokens
{
    public interface IInputPeeker<T>
    {
        T Peek();
        int Peek(T[] buffer, int numOfCharsToRead);
    }
}