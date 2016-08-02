namespace CmdCalculator.Tokens
{
    public interface IInputPeeker<T>
    {
        T EmptyValue { get; }
        T Peek();
        int Peek(T[] buffer, int numOfCharsToRead);
    }
}