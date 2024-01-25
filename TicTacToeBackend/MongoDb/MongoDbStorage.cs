using MongoDB.Driver;
using TemporaryStorage.Models;

namespace TemporaryStorage;

/// <summary>
/// Сервис с круд операциями к MongoDb
/// </summary>
/// <typeparam name="TEntity">модель сущности</typeparam>
public class MongoDbStorage<TEntity> : IMongoDbStorage<TEntity> where TEntity : IMongoDbEntity
{
	private readonly IMongoCollection<TEntity> _mongoCollection;

	public MongoDbStorage(IMongoDatabase? mongoDatabase)
	{
		_mongoCollection = mongoDatabase!.GetCollection<TEntity>(nameof(TEntity));
	}

	public async Task<TEntity?> GetByIdAsync(Guid id) =>
		await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

	public async Task InsertAsync(TEntity entity) =>
		await _mongoCollection.InsertOneAsync(entity);

	public async Task UpdateAsync(TEntity entity) =>
		await _mongoCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

	public async Task DeleteAsync(Guid id) =>
		await _mongoCollection.DeleteOneAsync(x => x.Id == id);
}