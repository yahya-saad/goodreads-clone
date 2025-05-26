namespace Goodreads.Application.Users.Commands.ChangePassword;
public class ChangePasswordCommand : IRequest<Result>
{
    public string CurrentPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}
