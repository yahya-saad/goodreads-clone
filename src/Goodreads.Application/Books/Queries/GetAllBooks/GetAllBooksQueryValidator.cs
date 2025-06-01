using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.Books.Queries.GetAllBooks;
public class GetAllBooksQueryValidator : AbstractValidator<GetAllBooksQuery>
{
    private readonly string[] allowedSortColumns = { "title", "language", "publisher" };

    public GetAllBooksQueryValidator()
    {
        RuleFor(x => x.Parameters)
          .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
            .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
            .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");
    }
}

