namespace CmdCalculator.Interfaces
{
    public interface ICalcualtorFactory<in TInput, out TOutput>
    {
        ICalculator<TInput, TOutput> CreateCalculator();
    }
}