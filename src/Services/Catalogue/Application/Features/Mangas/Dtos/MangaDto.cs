namespace MangaShelf.Catalogue.Application.Features.Mangas.Dtos;

public class MangaDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string AuthorId { get; set; } = null!;

    public string Description { get; set; } = null!;
}