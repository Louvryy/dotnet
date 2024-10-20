namespace Louvryy.Core.Domain.Interfaces.Core;

public interface IUseCase<TInput, TData>
{
    Task<TData> Execute(TInput input);
}

public interface IUseCase<TData>
{
    Task<TData> Execute();
}

public interface IUseCase
{
    Task Execute();
}
