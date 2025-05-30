using Goodreads.Application.Common.Attributes;
using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Goodreads.Application.Authors.Commands.CreateAuthor;
public class CreateAuthorCommand : IRequest<Result<string>>
{
    public string Name { get; set; }
    public string Bio { get; set; }
    [AllowedExtensions(ExtensionGroup.Image)]
    [MaxFileSize(AppConstants.MaxFileSizeInBytes)]
    public IFormFile ProfilePicture { get; set; }
};

