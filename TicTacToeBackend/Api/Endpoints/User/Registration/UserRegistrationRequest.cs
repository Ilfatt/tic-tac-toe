using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.User.Registration;

public record UserRegistrationRequest(
	[property: SwaggerSchema("Имя пользователя")]
	string Username,
	[property: SwaggerSchema("Пароль для нового аккаунта")]
	string Password);