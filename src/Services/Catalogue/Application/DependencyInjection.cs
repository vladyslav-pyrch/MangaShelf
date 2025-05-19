using FluentValidation;
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
        serviceCollection.AddScoped<ICommandHandler<CreateMangaCommand, Result<Guid>>, CreateMangaCommandHandler>();
    }

    public static void AddQueries(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IQueryHandler<GetMangaByIdQuery, MangaDto?>, GetMangaByIdQueryHandler>();
    }

    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
    }

    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCommands();
        serviceCollection.AddQueries();
        serviceCollection.AddValidators();
    }
}
