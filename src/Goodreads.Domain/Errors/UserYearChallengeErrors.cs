using SharedKernel;

namespace Goodreads.Domain.Errors;
public static class UserYearChallengeErrors
{
    public static Error NotFound(int year) => Error.NotFound(
       "YearChallenge.NotFound",
      $"Year challenge not found for year {year}");


}
