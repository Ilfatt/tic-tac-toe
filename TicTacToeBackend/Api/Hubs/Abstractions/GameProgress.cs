namespace Api.Hubs.Abstractions;

public class GameProgress
{
    public Guid GameId { get; set; }
    public Guid PreviousMoveUserId { get; set; }
    public int Move { get; set; } = 1;
    public GameMapSymbol[,] GameMap { get; set; } =
    {
        {
            GameMapSymbol.Empty, GameMapSymbol.Empty, GameMapSymbol.Empty
        },
        {
            GameMapSymbol.Empty, GameMapSymbol.Empty, GameMapSymbol.Empty
        },
        {
            GameMapSymbol.Empty, GameMapSymbol.Empty, GameMapSymbol.Empty
        }
    };
}