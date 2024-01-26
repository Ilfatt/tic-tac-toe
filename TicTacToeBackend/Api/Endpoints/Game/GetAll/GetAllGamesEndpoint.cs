using Api.Contracts;
using AutoMapper;
using Core.Features.Game.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.Game.GetAll;

public class GetAllGamesEndpoint : IEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("games/get-all", async (
                [FromQuery] int page,
                [FromQuery] int size,
                [FromServices] IMapper mapper,
                [FromServices] IMediator mediatr) =>
            {
                var response = await mediatr.Send(new GetAllGamesQuery(size, page));

                return response.HttpStatusCode switch
                {
                    200 => Results.Ok(mapper.Map<GetAllGamesResponse>(response)),
                    400 => Results.BadRequest(),
                    _ => Results.Problem(statusCode: 500)
                };
            })
            .RequireAuthorization()
            .WithOpenApi()
            .WithTags("Game")
            .WithSummary("Получение существующих игровых сессий")
            .WithMetadata(
                new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
                new SwaggerResponseAttribute(StatusCodes.Status400BadRequest,
                    "Не пройдена валидация. Username length должен быть от 5 до 30, password length от 5 до 30 "),
                new SwaggerResponseAttribute(
                    StatusCodes.Status200OK,
                    "",
                    typeof(GetAllGamesResponse)));
    }
}