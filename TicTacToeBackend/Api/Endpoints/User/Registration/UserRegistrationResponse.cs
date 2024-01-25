using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.User.Registration;

public record UserRegistrationResponse(
	[property: SwaggerSchema("Jwt токен")]
	string JwtToken = "");