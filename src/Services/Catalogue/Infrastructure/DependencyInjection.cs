using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Infrastructure.Cqrs;
using Microsoft.Extensions.DependencyInjection;

namespace MangaShelf.Catalogue.Infrastructure;

public static class DependencyInjection
{
    public static void AddCQRS(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICommandDispatcher, CommandDispatcher>();
    }
}
