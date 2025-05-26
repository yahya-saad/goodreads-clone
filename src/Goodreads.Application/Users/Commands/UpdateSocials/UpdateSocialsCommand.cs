namespace Goodreads.Application.Users.Commands.UpdateSocials;
public class UpdateSocialsCommand : IRequest<Result>
{
    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string LinkedIn { get; set; }
}

