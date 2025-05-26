using FluentValidation;

namespace Goodreads.Application.Users.Commands.UpdateUserProfile;
public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .MaximumLength(50);

        RuleFor(x => x.Bio)
            .MaximumLength(300);

        RuleFor(x => x.DateOfBirth)
            .Must(BeAValidDate)
            .When(x => x.DateOfBirth.HasValue)
            .WithMessage("Date of birth is not valid.");

        RuleFor(x => x.WebsiteUrl)
            .Must(BeAValidUrl)
            .When(x => !string.IsNullOrWhiteSpace(x.WebsiteUrl))
            .WithMessage("Website URL must be a valid HTTP/HTTPS URL.");

        RuleFor(x => x.Country)
            .MaximumLength(100);
    }

    private bool BeAValidUrl(string url)
        => Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
           && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

    private bool BeAValidDate(DateOnly? date) => date.HasValue && date.Value <= DateOnly.FromDateTime(DateTime.UtcNow);

}
