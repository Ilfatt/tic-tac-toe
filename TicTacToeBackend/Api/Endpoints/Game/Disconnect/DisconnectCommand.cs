using MediatR;

namespace Api.Endpoints.Game.Disconnect;

public record DisconnectCommand(Guid UserId) : ICommand<bool>;
