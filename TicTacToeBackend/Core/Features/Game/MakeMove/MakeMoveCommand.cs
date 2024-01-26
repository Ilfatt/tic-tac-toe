using MediatR;

namespace Core.Features.Game.MakeMove;

public record MakeMoveCommand(Guid UserId, int X, int Y) : ICommand<>;