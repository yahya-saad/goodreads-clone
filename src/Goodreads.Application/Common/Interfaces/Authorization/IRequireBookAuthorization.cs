namespace Goodreads.Application.Common.Interfaces.Authorization;
internal interface IRequireBookAuthorization
{
    string BookId { get; }
}