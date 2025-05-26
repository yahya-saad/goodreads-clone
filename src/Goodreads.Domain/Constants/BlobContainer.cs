using System.Text.Json.Serialization;

namespace Goodreads.Domain.Constants;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BlobContainer
{
    Users,
    Authors,
    Books
}