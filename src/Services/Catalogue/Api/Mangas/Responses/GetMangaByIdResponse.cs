namespace MangaShelf.Catalogue.Api.Mangas.Responses;

public class GetMangaByIdResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string AuthorId { get; set; } = null!;

    public string Description { get; set; } = null!;
}