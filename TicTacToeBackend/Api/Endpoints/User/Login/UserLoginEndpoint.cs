using Api.Contracts;
using AutoMapper;
using Core.Features.User.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.User.Login;

public class UserLoginEndpoint : IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app)
	{
		app.MapPost("auth/login", async (
				UserLoginRequest request,
				[FromServices] IMapper mapper,
				[FromServices] IMediator mediatr) =>
			{
				var response = await mediatr.Send(mapper.Map<UserLoginQuery>(request));

				return response.HttpStatusCode switch
				{
					200 => Results.Ok(mapper.Map<UserLoginResponse>(response)),
					400 => Results.BadRequest(),
					403 => Results.Forbid(),
					404 => Results.NotFound(),
					_ => Results.Problem(statusCode: 500)
				};
			})
			.AllowAnonymous()
			.WithOpenApi()
			.WithTags("User")
			.WithSummary("Вход по нику и паролю")
			.WithMetadata(
				new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
				new SwaggerResponseAttribute(
					StatusCodes.Status400BadRequest,
					"Не пройдена валидация. username - должен быть от 5 до 30 символов," +
					" password - должен быть от 5 до 30 символов"),
				new SwaggerResponseAttribute(
					StatusCodes.Status403Forbidden,
					"Пароль не верный"),
				new SwaggerResponseAttribute(
					StatusCodes.Status404NotFound,
					"Пользователь с таким username не найден"),
				new SwaggerResponseAttribute(
					StatusCodes.Status200OK,
					"",
					typeof(UserLoginResponse))
			);
	}
}