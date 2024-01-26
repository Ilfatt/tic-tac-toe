using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.Game.Join;

public record JoinGameRequest(
    [property: SwaggerSchema("Идентификатор игры")]
    Guid GameId);