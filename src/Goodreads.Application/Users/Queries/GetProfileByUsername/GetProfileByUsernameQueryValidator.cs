using FluentValidation;

namespace Goodreads.Application.Users.Queries.GetProfileByUsername;
public class GetProfileByUsernameQueryValidator : AbstractValidator<GetProfileByUsernameQuery>
{
    public GetProfileByUsernameQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is Required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters.");
    }
}
