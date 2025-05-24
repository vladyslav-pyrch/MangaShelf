using MangaShelf.Application.Abstractions;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Commands.ChangeDescription;

public record ChangeDescriptionCommand(Guid MangaId, string Description) : ICommand<Result<Unit>>;