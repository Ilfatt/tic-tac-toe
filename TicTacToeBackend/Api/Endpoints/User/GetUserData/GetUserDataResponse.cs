using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.User.GetUserData;

public record GetUserDataResponse(
	[property: SwaggerSchema("Ник пользоваетля")]
	string Username = "",
	[property: SwaggerSchema("Рейтинг пользователя")]
	int UserRating = 0);