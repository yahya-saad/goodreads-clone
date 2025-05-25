using FluentEmail.Core;
using Goodreads.Application.Common.Interfaces;

namespace Goodreads.Infrastructure.Services.EmailService;
internal class EmailService(IFluentEmail fluentEmail) : IEmailService
{
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        await fluentEmail
            .To(email)
            .Subject(subject)
            .Body(body, isHtml: true)
            .SendAsync();

    }

}
