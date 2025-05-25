namespace Goodreads.Application.Auth.Commands.RegisterUser;
public class RegisterUserCommand : IRequest<Result<string>>
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

