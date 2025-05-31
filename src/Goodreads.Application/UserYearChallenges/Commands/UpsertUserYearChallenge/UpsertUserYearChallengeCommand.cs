namespace Goodreads.Application.UserYearChallenges.Commands.UpdateUserYearChallenge;
public record UpsertUserYearChallengeCommand(int TargetBooksCount) : IRequest<Result>;
