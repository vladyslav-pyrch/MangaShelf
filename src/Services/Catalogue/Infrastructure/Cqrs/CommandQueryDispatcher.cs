using MangaShelf.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MangaShelf.Catalogue.Infrastructure.Cqrs;

public class CommandQueryDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher, IQueryDispatcher
{
    public Task DispatchCommand<TCommand>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand
    {
        var commandHandler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        return commandHandler.Handle(command, cancellationToken);
    }

    public Task<TResult> DispatchCommand<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
        where TCommand : ICommand<TResult>
    {
        var commandHandler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

        return commandHandler.Handle(command, cancellationToken);
    }

    public Task<TResult> DispatchQuery<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
        where TQuery : IQuery<TResult>
    {
        var queryHandler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

        return queryHandler.Handle(query, cancellationToken);
    }

    Task<TResult> IQueryDispatcher.Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) =>
        DispatchQuery<TQuery, TResult>(query, cancellationToken);

    Task<TResult> ICommandDispatcher.Dispatch<TCommand, TResult>(TCommand query, CancellationToken cancellationToken) =>
        DispatchCommand<TCommand, TResult>(query, cancellationToken);

    Task ICommandDispatcher.Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken) =>
        DispatchCommand(command, cancellationToken);
}