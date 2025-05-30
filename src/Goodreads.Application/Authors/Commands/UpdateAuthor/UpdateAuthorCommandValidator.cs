using FluentValidation;

namespace Goodreads.Application.Authors.Commands.UpdateAuthor;
internal class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("AuthorId is required.");

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("Author name must not exceed 100 characters.");

        RuleFor(x => x.Bio)
            .MaximumLength(1000)
            .WithMessage("Author bio must not exceed 1000 characters.");
    }
}
