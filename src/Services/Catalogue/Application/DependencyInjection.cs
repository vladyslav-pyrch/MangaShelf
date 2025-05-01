using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;
using MangaShelf.Catalogue.Application.Features.Mangas.Dtos;
using MangaShelf.Catalogue.Application.Features.Mangas.Queries.GetMangaById;
using Microsoft.Extensions.DependencyInjection;

namespace MangaShelf.Catalogue.Application;

public static class DependencyInjection
{
    public static void AddCommands(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICommandHandler<CreateMangaCommand, Guid>, CreateMangaCommandHandler>();
    }

    public static void AddQueries(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IQueryHandler<GetMangaByIdQuery, MangaDto?>, GetMangaByIdQueryHandler>();
    }
}
