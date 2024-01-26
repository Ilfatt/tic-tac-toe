using System.Security.Claims;
using Api.Services.Abstractions;

namespace Api.Services;

public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    private Guid? UserId => Guid.TryParse(
        httpContextAccessor.HttpContext
            ?.User
            .FindFirstValue(ClaimTypes.NameIdentifier),
        out var userId)
        ? userId
        : null;

    public Guid? GetUserIdOrDefault() => UserId;
}