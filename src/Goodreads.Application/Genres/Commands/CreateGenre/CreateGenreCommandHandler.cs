namespace Goodreads.Application.Genres.Commands.CreateGenre;

internal class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateGenreCommandHandler> _logger;

    public CreateGenreCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateGenreCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new genre with name: {GenreName}", request.Name);
        var genre = new Genre { Name = request.Name.ToLower() };
        await _unitOfWork.Genres.AddAsync(genre);
        await _unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(genre.Id);
    }
}
