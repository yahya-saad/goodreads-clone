using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.Genres.Commands.UpdateGenre;

internal class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateGenreCommandHandler> _logger;

    public UpdateGenreCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateGenreCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _unitOfWork.Genres.GetByIdAsync(request.Id);
        if (genre is null)
        {
            _logger.LogWarning("Genre with ID {GenreId} not found.", request.Id);
            return Result.Fail(GenreErrors.NotFound(request.Id));
        }

        genre.Name = request.Name.ToLower();
        _unitOfWork.Genres.Update(genre);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Updated genre with ID: {GenreId}", request.Id);
        return Result.Ok();
    }
}