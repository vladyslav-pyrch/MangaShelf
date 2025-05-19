using MangaShelf.Application.Abstractions;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;

public record CreateMangaCommand(string Name) : ICommand<Result<Guid>>;