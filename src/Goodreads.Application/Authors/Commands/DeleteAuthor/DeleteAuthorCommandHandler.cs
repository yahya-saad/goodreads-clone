using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.Authors.Commands.DeleteAuthor;
internal class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private readonly ILogger<DeleteAuthorCommandHandler> _logger;

    public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork, IBlobStorageService blobStorageService, ILogger<DeleteAuthorCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
        _logger = logger;
    }
    public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorId = request.Id;
        _logger.LogInformation("Deleting author with ID: {AuthorId}", authorId);
        var author = await _unitOfWork.Authors.GetByIdAsync(authorId);
        if (author == null)
        {
            _logger.LogWarning("Author with ID: {AuthorId} not found", authorId);
            return Result.Fail(AuthorErrors.NotFound(authorId));
        }

        await _blobStorageService.DeleteAsync(author.ProfilePictureBlobName);
        _unitOfWork.Authors.Delete(author);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}
