namespace CmdCalculator.Interfaces
{
    public interface ICalculator<in TInput, out TOutput>
    {
        TOutput Calculate(TInput input);
    }
}
