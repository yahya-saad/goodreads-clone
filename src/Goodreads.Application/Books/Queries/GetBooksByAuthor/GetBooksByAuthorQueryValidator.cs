using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.Books.Queries.GetBooksByAuthor;
internal class GetBooksByAuthorQueryValidator : AbstractValidator<GetBooksByAuthorQuery>
{
    private readonly string[] allowedSortColumns = { "title", "language", "publisher" };

    public GetBooksByAuthorQueryValidator()
    {
        RuleFor(x => x.AuthorId).NotEmpty();

        RuleFor(x => x.Parameters)
          .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.Query)
            .NotEmpty().WithMessage("Query must not be empty");

        RuleFor(x => x.Parameters.SortColumn)
         .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
         .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");
    }
}
