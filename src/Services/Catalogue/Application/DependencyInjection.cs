using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;
using Microsoft.Extensions.DependencyInjection;

namespace MangaShelf.Catalogue.Application;

public static class DependencyInjection
{
    public static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateMangaCommand, Guid>, CreateMangaCommandHandler>();
    }
}
