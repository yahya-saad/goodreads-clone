namespace Goodreads.Application.Quotes.Commands.CreateQuote;
public class CreateQuoteCommand : IRequest<Result<string>>
{
    public string Text { get; set; } = default!;
    public string AuthorId { get; set; } = default!;
    public string BookId { get; set; } = default!;
    public List<string>? Tags { get; set; }
}
