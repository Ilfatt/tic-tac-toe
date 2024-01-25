namespace TemporaryStorage.Models;

/// <summary>
/// Контракт для сущности MongoDb
/// </summary>
public interface IMongoDbEntity
{
	/// <summary>
	/// Id записи
	/// </summary>
	public Guid Id { get; init; }
}