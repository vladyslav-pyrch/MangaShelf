using MangaShelf.Catalogue.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace MangaShelf.Catalogue.Infrastructure.Persistence;

public class CatalogueDbContext(DbContextOptions<CatalogueDbContext> options) : DbContext(options)
{
    public DbSet<MangaEntity> Mangas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MangaEntity>()
            .HasKey(entity => entity.Id);

        modelBuilder.Entity<MangaEntity>()
            .Property(entity => entity.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<MangaEntity>()
            .Property(entity => entity.Name)
            .IsRequired();

        modelBuilder.Entity<MangaEntity>()
            .Property(entity => entity.AuthorId)
            .IsRequired();
    }
}