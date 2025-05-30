using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.Genres.Commands.DeleteGenre;

internal class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteGenreCommandHandler> _logger;

    public DeleteGenreCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteGenreCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genreId = request.Id;
        _logger.LogInformation("Deleting genre with ID: {GenreId}", genreId);
        var genre = await _unitOfWork.Genres.GetByIdAsync(genreId);
        if (genre == null)
        {
            _logger.LogWarning("Genre with ID: {GenreId} not found", genreId);
            return Result.Fail(GenreErrors.NotFound(genreId));
        }

        _unitOfWork.Genres.Delete(genre);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}
