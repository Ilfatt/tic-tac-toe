namespace TemporaryStorage.Models;

/// <summary>
/// Рейтинг пользователя
/// </summary>
public class UserRating : IMongoDbEntity
{
	/// <summary>
	/// Id пользователя
	/// </summary>
	public required Guid Id { get; init; }

	/// <summary>
	/// Рейтинг пользователя
	/// </summary>
	public required int Rating { get; set; }
}