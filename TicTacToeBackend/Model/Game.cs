using Microsoft.AspNetCore.Identity;

namespace Model;

/// <summary>
/// Лобби
/// </summary>
public class Game
{
	/// <summary>
	/// id игры 
	/// </summary>
	public Guid Id { get; set; }
	
	/// <summary>
	/// Игрок1
	/// </summary>
	public required IdentityUser<Guid>? Player1 { get; set; }

	/// <summary>
	/// id Игрока1
	/// </summary>
	public required Guid? Player1Id { get; set; }

	/// <summary>
	/// Игрок1
	/// </summary>
	public IdentityUser<Guid>? Player2 { get; set; }

	/// <summary>
	/// id Игрока2
	/// </summary>
	public Guid? Player2Id { get; set; }

	/// <summary>
	/// Максимальный рейтинг для лобби
	/// </summary>
	public required int MaxRating { get; set; }

	/// <summary>
	/// Карта игры, -1 -пусто, 0 - 0, 1 - X. 
	/// </summary>
	public required int[] Map { get; init; } = { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
}