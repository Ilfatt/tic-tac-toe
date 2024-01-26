using AutoMapper;
using Core.Features.Game.Join;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.Game.Join;

public class JoinGameEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("games/create", async (
                JoinGameRequest request,
                [FromServices] IMapper mapper,
                [FromServices] IMediator mediatr) =>
            {
                var response = await mediatr.Send(mapper.Map<JoinGameCommand>(request));

                return response.HttpStatusCode switch
                {
                    200 => Results.Ok(mapper.Map<JoinGameResponse>(response)),
                    400 => Results.BadRequest(),
                    _ => Results.Problem(statusCode: 500)
                };
            })
            .RequireAuthorization()
            .WithOpenApi()
            .WithTags("User")
            .WithSummary("Подключение к игровой сессии.")
            .WithMetadata(
                new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError),
                new SwaggerResponseAttribute(StatusCodes.Status400BadRequest,
                    "Не пройдена валидация."),
                new SwaggerResponseAttribute(
                    StatusCodes.Status200OK,
                    "",
                    typeof(JoinGameResponse)));
    }
}