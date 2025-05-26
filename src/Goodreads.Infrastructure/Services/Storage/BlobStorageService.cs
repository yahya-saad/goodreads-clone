using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Constants;
using Microsoft.Extensions.Options;

namespace Goodreads.Infrastructure.Services.Storage;
internal class BlobStorageService(IOptions<BlobStorageSettings> options) : IBlobStorageService
{
    private readonly BlobStorageSettings blobStorageSettings = options.Value;

    public async Task<(string Url, string BlobName)> UploadAsync(string FileName, Stream Data, BlobContainer Container)
    {
        var client = new BlobServiceClient(blobStorageSettings.ConnectionString.Trim());
        var container = client.GetBlobContainerClient(blobStorageSettings.ContainerName.ToLower());

        await container.CreateIfNotExistsAsync(PublicAccessType.Blob);

        string folderName = GetFolderName(Container);

        var uniqueFileName = $"{folderName}/{Guid.NewGuid()}{Path.GetExtension(FileName)}";
        var blobClient = container.GetBlobClient(uniqueFileName);

        await blobClient.UploadAsync(Data, overwrite: true);

        return (blobClient.Uri.ToString(), uniqueFileName);
    }

    public async Task DeleteAsync(string blobName)
    {
        if (blobName is null) return;
        var client = new BlobServiceClient(blobStorageSettings.ConnectionString);
        var container = client.GetBlobContainerClient(blobStorageSettings.ContainerName);

        var blobClient = container.GetBlobClient(blobName);
        await blobClient.DeleteIfExistsAsync();
    }

    public string? GetUrl(string blobName)
    {
        if (string.IsNullOrEmpty(blobName))
        {
            return null;
        }

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = blobStorageSettings.ContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
            BlobName = blobName
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var credintials = new StorageSharedKeyCredential(blobStorageSettings.AccountName, blobStorageSettings.AccountKey);
        var sasToken = sasBuilder.ToSasQueryParameters(credintials).ToString();

        var serviceClient = new BlobServiceClient(blobStorageSettings.ConnectionString);
        var containerClient = serviceClient.GetBlobContainerClient(blobStorageSettings.ContainerName.ToLower());
        var blobClient = containerClient.GetBlobClient(blobName);

        return $"{blobClient.Uri}?{sasToken}";
    }

    private string GetFolderName(BlobContainer container)
    {
        return container switch
        {
            BlobContainer.Users => "users/",
            BlobContainer.Authors => "authors/",
            BlobContainer.Books => "books/",
            _ => "others"
        };
    }

}
