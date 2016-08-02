using System;

namespace CmdCalculator.Tokens
{
    public interface IInputReaderFactory<in TInput,TOutput>
        where TOutput : IEquatable<TOutput>
    {
        IInputReader<TOutput> Create(TInput input);
    }
}