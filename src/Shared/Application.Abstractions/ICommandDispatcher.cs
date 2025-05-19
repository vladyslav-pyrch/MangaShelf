namespace MangaShelf.Application.Abstractions;

public interface ICommandDispatcher
{
    public Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand<TResult>;
}