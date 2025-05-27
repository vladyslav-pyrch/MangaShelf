namespace MangaShelf.Catalogue.Api.Mangas.Requests;

public class CreateMangaRequest
{
    public string Title { get; set; } = null!;

    public string AuthorId { get; set; } = null!;
}