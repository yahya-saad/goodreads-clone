using Goodreads.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Goodreads.Infrastructure.Identity;
internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public string? UserId =>
     httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}
