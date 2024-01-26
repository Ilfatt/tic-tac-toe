using Api.Contracts;
using Api.Extensions;
using AutoMapper;
using Core.Features.Game.Create;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.Game.Create;

public class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
{
	public CreateGameRequestValidator()
	{
		RuleFor(x => x.RateRange)
			.Must(x => x.Min >= 0)
			.Must(x => x.Max >= 20)
			.Must(x => x.Max - x.Min > 20);
	}
}

public class CreateGameEndpoint : IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app)
	{
		app.MapPost("games/create", async (
				CreateGameRequest request,
				[FromServices] IMapper mapper,
				[FromServices] IMediator mediatr,
				HttpContext httpContext) =>
			{
				var command = new CreateGameCommand(request.RateRange, httpContext.GetUserId());
				var response = await mediatr.Send(command);

				return response.HttpStatusCode switch
				{
					200 => Results.Ok(mapper.Map<CreateGameResponse>(response)),
					400 => Results.BadRequest(),
					_ => Results.Problem(statusCode: 500)
				};
			})
			.RequireAuthorization()
			.WithOpenApi()
			.WithTags("Game")
			.WithSummary("Создание игровой сессии")
			.WithMetadata(
				new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
				new SwaggerResponseAttribute(StatusCodes.Status400BadRequest,
					"Не пройдена валидация."),
				new SwaggerResponseAttribute(
					StatusCodes.Status200OK,
					"",
					typeof(CreateGameResponse)));
	}
}