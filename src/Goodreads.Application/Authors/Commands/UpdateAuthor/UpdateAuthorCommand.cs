using Goodreads.Application.Common.Attributes;
using Goodreads.Application.Common.Interfaces.Authorization;
using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Goodreads.Application.Authors.Commands.UpdateAuthor;
public class UpdateAuthorCommand : IRequest<Result>, IRequireAuthorAuthorization
{
    public string AuthorId { get; set; }
    public string? Name { get; set; }
    public string? Bio { get; set; }
    [AllowedExtensions(ExtensionGroup.Image)]
    [MaxFileSize(AppConstants.MaxFileSizeInBytes)]
    public IFormFile? ProfilePicture { get; set; }
}
