namespace MangaShelf.Catalogue.Api.Mangas.Requests;

public class CreateMangaRequest
{
    public string Name { get; set; } = null!;

    public string OwnerId { get; set; } = null!;
}