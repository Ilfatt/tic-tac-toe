using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.User.Login;

public record UserLoginResponse(
	[property: SwaggerSchema("Рефреш токен")]
	string JwtToken = "",
	[property: SwaggerSchema("Ник пользователя")]
	string Username = "",
	[property: SwaggerSchema("Рейтинг пользователя")]
	int UserRating = 0);