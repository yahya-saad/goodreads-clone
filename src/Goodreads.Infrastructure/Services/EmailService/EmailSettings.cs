namespace Goodreads.Infrastructure.Services.EmailService;
internal class EmailSettings
{
    public const string Section = "EmailSettings";
    public bool UseSmtp4Dev { get; set; }
    public string FromEmail { get; set; } = default!;
    public string FromName { get; set; } = default!;
    public string Host { get; set; } = default!;
    public int Port { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
