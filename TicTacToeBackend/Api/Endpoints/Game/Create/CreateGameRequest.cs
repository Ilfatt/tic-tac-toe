using Core.Features.Game;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Endpoints.Game.Create;

public record CreateGameRequest(
    [property: SwaggerSchema("Требуемый рейтинг")]
    RateRange RateRange);