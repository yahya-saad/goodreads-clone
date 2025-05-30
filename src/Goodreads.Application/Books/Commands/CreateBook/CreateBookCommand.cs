using Goodreads.Application.Common.Attributes;
using Goodreads.Application.Common.Interfaces.Authorization;
using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Goodreads.Application.Books.Commands.CreateBook;
public class CreateBookCommand : IRequest<Result<string>>, IRequireAuthorAuthorization
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string Language { get; set; }
    public int PageCount { get; set; }
    public string Publisher { get; set; }
    public string AuthorId { get; set; }

    [AllowedExtensions(ExtensionGroup.Image)]
    [MaxFileSize(AppConstants.MaxFileSizeInBytes)]
    public IFormFile? CoverImage { get; set; }
}
