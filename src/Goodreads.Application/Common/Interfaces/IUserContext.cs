namespace Goodreads.Application.Common.Interfaces;
public interface IUserContext
{
    string? UserId { get; }
    bool IsInRole(string role);
}
