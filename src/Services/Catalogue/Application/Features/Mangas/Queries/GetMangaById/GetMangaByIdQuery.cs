using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Application.Features.Mangas.Dtos;

namespace MangaShelf.Catalogue.Application.Features.Mangas.Queries.GetMangaById;

public record GetMangaByIdQuery(Guid Id) : IQuery<MangaDto?>;