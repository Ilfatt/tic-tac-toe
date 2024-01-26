using Core.Features.Game;

namespace Model;

/// <summary>
/// Лобби
/// </summary>
public class Game
{
	/// <summary>
	/// Идентификатор
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Идентификатор хоста
	/// </summary>
	public required Guid OwnerId { get; set; }

	/// <summary>
	/// Идентификатор соперника
	/// </summary>
	public Guid? OpponentId { get; set; }

	/// <summary>
	/// Минимальный требуемый рейтинг
	/// </summary>
	public required int MinRate { get; set; }
	
	/// <summary>
	/// Максимальный возможный рейтинг
	/// </summary>
	public required int MaxRate { get; set; }
	
	/// <summary>
	/// Статус игры
	/// </summary>
	public required GameState GameState { get; set; }
	
	/// <summary>
	/// Карта игры
	/// </summary>
	public required GameMapSymbol[] GameMap { get; set; }
}