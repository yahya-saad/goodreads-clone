namespace Goodreads.Application.DTOs;
public record UpdateQuoteRequest(string Text, List<string>? Tags);
