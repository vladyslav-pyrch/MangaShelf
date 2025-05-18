using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Application.Features.Mangas.Queries;
using MangaShelf.Catalogue.Domain.Mangas;
using MangaShelf.Catalogue.Infrastructure.Cqrs;
using MangaShelf.Catalogue.Infrastructure.Persistence;
using MangaShelf.Catalogue.Infrastructure.Persistence.QueryServices;
using MangaShelf.Catalogue.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MangaShelf.Catalogue.Infrastructure;

public static class DependencyInjection
{
    public static void AddCqrs(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICommandDispatcher, CommandQueryDispatcher>();
        serviceCollection.AddScoped<IQueryDispatcher, CommandQueryDispatcher>();
    }

    public static void AddPersistence(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContextFactory<CatalogueDbContext>((_, builder) =>
        {
            builder.UseInMemoryDatabase("CatalogueInMemoryDb");
        });
        serviceCollection.AddScoped<IMangaRepository, MangaRepository>();
        serviceCollection.AddScoped<IMangaQueryService, MangaQueryService>();
    }

    public static void AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCqrs();
        serviceCollection.AddPersistence();
    }
}
