using Goodreads.Application.DTOs;

namespace Goodreads.Application.UserYearChallenges.Queries.GetUserYearChallenge;
public record GetUserYearChallengeQuery(string UserId, int Year) : IRequest<Result<UserYearChallengeDetailsDto>>;