using FluentAssertions;
using MangaShelf.Catalogue.Domain.Mangas;
using Xunit.Abstractions;

namespace MangaShelf.Catalogue.Domain.Tests.Mangas;

public class MangaTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task GivenNameIsNull_WhenCreatingManga_ThenThrowArgumentNullException()
    {
        // Given
        var id = new MangaId(Guid.NewGuid());
        string name = null!;

        // When
        Func<Manga> when = () => Manga.Create(id, name);

        // Then
        ArgumentNullException? which = when.Should().Throw<ArgumentNullException>().Which;
        which.ParamName.Should().Be(nameof(Manga.Name));
        testOutputHelper.WriteLine(which.Message);
    }
}