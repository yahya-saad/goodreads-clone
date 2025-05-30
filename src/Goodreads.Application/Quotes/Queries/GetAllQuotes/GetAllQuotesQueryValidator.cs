using FluentValidation;
using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.Quotes.Queries.GetAllQuotes;

public class GetAllQuotesQueryValidator : AbstractValidator<GetAllQuotesQuery>
{
    private readonly string[] allowedSortColumns = { "text", "createdat", "likescount" };

    public GetAllQuotesQueryValidator()
    {

        RuleFor(x => x.Parameters)
            .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
            .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
            .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");

        RuleFor(x => x.UserId)
               .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
               .WithMessage("UserId must be a valid GUID");

        RuleFor(x => x.AuthorId)
            .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
            .WithMessage("AuthorId must be a valid GUID");

        RuleFor(x => x.BookId)
            .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
            .WithMessage("BookId must be a valid GUID");
    }
}