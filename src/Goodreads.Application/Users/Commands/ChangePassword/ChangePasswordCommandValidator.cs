namespace Goodreads.Application.Users.Commands.ChangePassword;
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword)
             .NotEmpty().WithMessage("CurrentPassword is required");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("NewPassword is required")
            .MinimumLength(6).WithMessage("NewPassword must be at least 6 characters long.")
            .NotEqual(x => x.CurrentPassword).WithMessage("New password must be different from current password.");


        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword).WithMessage("The new password and confirmation password do not match.");
    }
}
