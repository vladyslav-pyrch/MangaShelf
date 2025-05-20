using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MangaShelf.Application.Abstractions;
using MangaShelf.Catalogue.Application.Extensions;
using MangaShelf.Catalogue.Application.Features.Mangas.Commands.CreateManga;

namespace MangaShelf.Catalogue.Application.Tests.Features.Mangas.Commands.CreateManga;

public class CreateMangaCommandValidationTests
{
    private readonly IValidator<CreateMangaCommand> _validator = new CreateMangaCommandValidator();

    [Fact]
    public async Task GivenNameIsLongerThan50Characters_WhenValidating()
    {
        var name = new string('a', 51);
        var command = new CreateMangaCommand(name);

        ValidationResult result = await _validator.ValidateAsync(command);
        Result<Unit> failure = result.ToFailure<Unit>();

        failure.IsFailure.Should().BeTrue();
        failure.Errors.Should().Contain(error => error.ErrorCode == ErrorCodes.InvalidMangaName);
    }

    [Fact]
    public async Task GivenEmptyName_WhenValidating()
    {
        var name = string.Empty;
        var command = new CreateMangaCommand(name);

        ValidationResult result = await _validator.ValidateAsync(command);
        Result<Unit> failure = result.ToFailure<Unit>();

        failure.IsFailure.Should().BeTrue();
        failure.Errors.Should().Contain(error => error.ErrorCode == ErrorCodes.InvalidMangaName);
    }
}