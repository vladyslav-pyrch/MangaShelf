namespace MangaShelf.Catalogue.Infrastructure.Persistence.Entities;

public class MangaEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string AuthorId { get; set; }
}