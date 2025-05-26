namespace Goodreads.Infrastructure.Services.Storage;
internal class BlobStorageSettings
{
    public const string Section = "BlobStorageSettings";
    public string ConnectionString { get; set; } = default!;
    public string ContainerName { get; set; } = default!;
    public string AccountName { get; set; } = default!;
    public string AccountKey { get; set; } = default!;

}
