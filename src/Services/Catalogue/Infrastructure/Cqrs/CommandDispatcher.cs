using MangaShelf.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MangaShelf.Catalogue.Infrastructure.Cqrs;

public class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        var commandHandler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        await commandHandler.Handle(command, cancellationToken);
    }

    public async Task<TResult> Dispatch<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand<TResult>
    {
        var commandHandler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

        return await commandHandler.Handle(command, cancellationToken);
    }
}