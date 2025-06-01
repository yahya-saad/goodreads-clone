using Goodreads.Application.Common.Validation;

namespace Goodreads.Application.Books.Queries.GetBooksByGenre;
public class GetBooksByGenreQueryValidator : AbstractValidator<GetBooksByGenreQuery>
{
    private readonly string[] allowedSortColumns = { "title", "language", "publisher" };

    public GetBooksByGenreQueryValidator()
    {

        RuleFor(x => x.Parameters)
            .SetValidator(new QueryParametersValidator());

        RuleFor(x => x.Parameters.SortColumn)
         .Must(column => string.IsNullOrEmpty(column) || allowedSortColumns.Contains(column.ToLower()))
         .WithMessage($"Sort column must be one of the following: {string.Join(", ", allowedSortColumns)}");
    }
}
