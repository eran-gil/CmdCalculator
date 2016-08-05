namespace CmdCalculator.Interfaces.Tokens
{
    public interface IInputReader<T> : IInputPeeker<T>
    {
        T Read();
        int Read(T[] buffer, int numOfCharsToRead);
    }
}