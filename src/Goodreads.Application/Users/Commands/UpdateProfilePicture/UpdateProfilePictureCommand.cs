using Goodreads.Application.Common.Attributes;
using Microsoft.AspNetCore.Http;
using SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace Goodreads.Application.Users.Commands.UpdateProfilePicture;
public class UpdateProfilePictureCommand : IRequest<Result>
{
    [Required]
    [AllowedExtensions(ExtensionGroup.Image)]
    [MaxFileSize(AppConstants.MaxFileSizeInBytes)]
    public IFormFile File { get; set; }
}