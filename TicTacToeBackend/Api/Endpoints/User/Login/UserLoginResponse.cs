using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.User.Login;

public record UserLoginResponse(
	[property: SwaggerSchema("Рефреш токен")]
	string JwtToken = "");