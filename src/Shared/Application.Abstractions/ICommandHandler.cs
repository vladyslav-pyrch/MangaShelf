namespace MangaShelf.Application.Abstractions;

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    public Task<TResult> Handle(TCommand command, CancellationToken cancellationToken = default);
}