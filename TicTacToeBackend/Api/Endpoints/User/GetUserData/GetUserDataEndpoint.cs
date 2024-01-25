using Api.Contracts;
using Api.Extensions;
using AutoMapper;
using Core.Features.User.GetUserData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.User.GetUserData;

public class GetUserDataEndpoint : IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app)
	{
		app.MapGet("user/getUserData", async (
				[FromServices] IMediator mediator,
				[FromServices] IMapper mapper,
				[FromServices] IHttpContextAccessor httpContextAccessor) =>
			{
				var response = await mediator.Send(
					new GetUserDataQuery(httpContextAccessor.HttpContext!.GetUserId()));
				
				return response.HttpStatusCode switch
				{
					200 => Results.Ok(mapper.Map<GetUserDataResponse>(response)), 
					_ => Results.Problem(statusCode: 500)
				};
			})
			.RequireAuthorization()
			.WithTags("User")
			.WithSummary("Получение рейтинга и ника пользоваетля")
			.WithDescription("Доступ: только авторизованные пользователи")
			.WithMetadata(
				new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
				new SwaggerResponseAttribute(StatusCodes.Status401Unauthorized, "Пользователь не авторизован"),
				new SwaggerResponseAttribute(StatusCodes.Status200OK), typeof(GetUserDataResponse),
				new SwaggerResponseAttribute(StatusCodes.Status403Forbidden), "Jwt токен устарел")
			.WithOpenApi();
	}
}