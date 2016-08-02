namespace CmdCalculator.Evaluations
{
    public interface IExpressionVisitorFactory<out T>
    {
        IExpressionVisitor<T> Create();
    }
}