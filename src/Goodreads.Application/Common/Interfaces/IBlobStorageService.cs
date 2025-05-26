using Goodreads.Domain.Constants;

namespace Goodreads.Application.Common.Interfaces;
public interface IBlobStorageService
{
    Task<(string Url, string BlobName)> UploadAsync(string FileName, Stream Data, BlobContainer Container);
    string? GetUrl(string blobName);
    Task DeleteAsync(string blobName);
}