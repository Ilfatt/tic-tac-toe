using Api.Contracts;
using Api.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;

namespace Api.Endpoints.Game.MakeMove;

public class MakeMoveEndpoint : IEndpoint
{
	public void AddRoute(IEndpointRouteBuilder app)
	{
		app.MapPut("/game/move", async (
				MakeMoveRequest request,
				[FromServices] IBus bus,
				HttpContext httpContext) =>
			{
				await bus.Publish(new MoveDto(request.Index, httpContext.GetUserId()));
			})
			.RequireAuthorization();
	}
}