using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.UserYearChallenges.Queries.GetAllUserYearChallenges;
public record GetAllUserYearChallengesQuery(string UserId, QueryParameters Parameters, int? Year) : IRequest<PagedResult<UserYearChallengeDto>>;
