namespace MangaShelf.Catalogue.Api.Mangas.Requests;

public class AddVolumeRequest
{
    public string Name { get; set; } = null!;

    public int Order { get; set; }
}