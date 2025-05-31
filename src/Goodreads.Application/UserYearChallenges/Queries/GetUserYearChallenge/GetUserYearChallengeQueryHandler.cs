using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.DTOs;
using Goodreads.Domain.Constants;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.UserYearChallenges.Queries.GetUserYearChallenge;
public class GetUserYearChallengeQueryHandler : IRequestHandler<GetUserYearChallengeQuery, Result<UserYearChallengeDetailsDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserYearChallengeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserYearChallengeDetailsDto>> Handle(GetUserYearChallengeQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var year = request.Year;

        var (challenges, _) = await _unitOfWork.UserYearChallenges.GetAllAsync(
            filter: c => c.UserId == userId && c.Year == year
        );

        var challenge = challenges.FirstOrDefault();
        if (challenge == null)
            return Result<UserYearChallengeDetailsDto>.Fail(UserYearChallengeErrors.NotFound(year));

        var (books, count) = await _unitOfWork.BookShelves.GetAllAsync(
            filter: bs =>
                bs.Shelf.UserId == userId &&
                bs.Shelf.IsDefault &&
                bs.Shelf.Name == DefaultShelves.Read &&
                bs.AddedAt.Year == year,
            includes: new[] { "Book.Author" }
        );


        var challengeDto = _mapper.Map<UserYearChallengeDetailsDto>(challenge);
        challengeDto.Books = _mapper.Map<List<ChallengeBookDto>>(books);

        return Result<UserYearChallengeDetailsDto>.Ok(challengeDto);
    }
}
