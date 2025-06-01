namespace Goodreads.Application.Authors.Commands.CreateAuthor;
public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Author name is required.")
            .MaximumLength(100).WithMessage("Author name must not exceed 100 characters.");

        RuleFor(x => x.Bio)
            .NotEmpty().WithMessage("Author bio is required.")
            .MaximumLength(1000).WithMessage("Author bio must not exceed 1000 characters.");
    }
}
