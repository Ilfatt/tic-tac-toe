using Api.Contracts;
using AutoMapper;
using Core.Features.User.Registration;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.User.Registration;

public class RegisterUserEndpoint : IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app)
	{
		app.MapPost("user/registration", async (
				UserRegistrationRequest request,
				[FromServices] IMapper mapper,
				[FromServices] IMediator mediatr) =>
			{
				var response = await mediatr.Send(mapper.Map<UserRegistrationCommand>(request));

				return response.HttpStatusCode switch
				{
					200 => Results.Ok(mapper.Map<UserRegistrationResponse>(response)),
					400 => Results.BadRequest(),
					409 => Results.Conflict(),
					_ => Results.Problem(statusCode: 500)
				};
			})
			.AllowAnonymous()
			.WithOpenApi()
			.WithTags("User")
			.WithSummary("Регистрация пользователя")
			.WithMetadata(
				new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
				new SwaggerResponseAttribute(StatusCodes.Status400BadRequest,
					"Не пройдена валидация. Username length должен быть от 5 до 30, password length от 5 до 30 "),
				new SwaggerResponseAttribute(StatusCodes.Status409Conflict,
					"Пользователь с таким Username уже существует"),
				new SwaggerResponseAttribute(
					StatusCodes.Status200OK,
					"",
					typeof(UserRegistrationResponse)));
	}
}