namespace Core.Features.Game;

public struct GameProgress
{ 
    public Guid? MoveUserId { get; set; }
    
    public GameMapSymbol[] GameMap { get; set; }
}