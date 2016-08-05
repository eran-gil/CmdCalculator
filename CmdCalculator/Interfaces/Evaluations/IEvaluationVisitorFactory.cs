namespace CmdCalculator.Interfaces.Evaluations
{
    public interface IEvaluationVisitorFactory<out T>
    {
        IEvaluationVisitor<T> Create();
    }
}