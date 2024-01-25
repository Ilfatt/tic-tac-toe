using System.Security.Claims;

namespace Api.Extensions;

public static class HttpContextExtensions
{
	/// <summary>
	/// Получить id пользователя из httpContext
	/// </summary>
	/// <param name="httpContext">HttpContext</param>
	/// <returns>id пользователя</returns>
	/// <exception cref="ArgumentException">Пользователь не авторизирован или нет клэйма
	/// с типом ClaimTypes.NameIdentifier</exception>
	public static Guid GetUserId(this HttpContext httpContext)
	{
		if (httpContext.User.Identity is { IsAuthenticated: false })
			throw new ArgumentException("User not authenticated");

		var claim = httpContext.User.Claims
			            .First(claim => claim.Type == ClaimTypes.NameIdentifier)
		            ?? throw new ArgumentException($"Authenticated user has not claim" +
		                                           $" with type '{ClaimTypes.NameIdentifier}'");

		return new Guid(claim.Value);
	}
}