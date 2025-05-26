using FluentValidation;

namespace MangaShelf.Catalogue.Api.Mangas.Requests;

public class ChangeDescriptionRequestValidator : AbstractValidator<ChangeDescriptionRequest>
{
    public ChangeDescriptionRequestValidator()
    {
        RuleFor(request => request.Description)
            .MaximumLength(5000).WithMessage($"{nameof(ChangeDescriptionRequest.Description)} must not exceed 5000.");
    }
}