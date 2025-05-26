using FluentValidation;
namespace Goodreads.Application.Users.Commands.UpdateSocials;

public class UpdateSocialsCommandValidator : AbstractValidator<UpdateSocialsCommand>
{
    public UpdateSocialsCommandValidator()
    {
        RuleFor(x => x.Twitter)
            .MaximumLength(100)
            .Must(BeAValidUrl).WithMessage("Twitter URL must be valid HTTP/HTTPS URL.");

        RuleFor(x => x.Facebook)
            .MaximumLength(100)
            .Must(BeAValidUrl).WithMessage("GitHub URL must be valid HTTP/HTTPS URL.");

        RuleFor(x => x.LinkedIn)
            .MaximumLength(100)
            .Must(BeAValidUrl).WithMessage("LinkedIn URL must be valid HTTP/HTTPS URL.");

    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}

