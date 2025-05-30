using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Constants;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.Authors.Commands.UpdateAuthor;
internal class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateAuthorCommandHandler> _logger;
    private readonly IBlobStorageService _blobStorageService;

    public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateAuthorCommandHandler> logger, IBlobStorageService blobStorageService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorId = request.AuthorId;
        _logger.LogInformation("Updating author with ID: {AuthorId}", authorId);
        var author = await _unitOfWork.Authors.GetByIdAsync(authorId);
        if (author == null)
        {
            _logger.LogWarning("Author with ID: {AuthorId} not found", authorId);
            return Result.Fail(AuthorErrors.NotFound(authorId));
        }

        if (!string.IsNullOrEmpty(request.Name))
            author.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Bio))
            author.Bio = request.Bio;

        if (request.ProfilePicture != null)
        {
            await _blobStorageService.DeleteAsync(author.ProfilePictureBlobName);

            using var stream = request.ProfilePicture.OpenReadStream();
            var (url, blobName) = await _blobStorageService.UploadAsync(request.ProfilePicture.FileName, stream, BlobContainer.Authors);
            author.ProfilePictureUrl = url;
            author.ProfilePictureBlobName = blobName;
        }

        _unitOfWork.Authors.Update(author);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Author with ID: {AuthorId} updated successfully", authorId);
        return Result.Ok();
    }
}