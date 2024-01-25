using MongoDB.Driver;
using TemporaryStorage;
using TemporaryStorage.Models;

namespace Api.Extensions;

public static class MongoDbConfiguration
{
	public static void AddMongoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
	{
		var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
		var database = client.GetDatabase(configuration["DbConfigs:MongoDbName"]);

		serviceCollection.AddSingleton<IMongoDbStorage<UserRating>>(
			new MongoDbStorage<UserRating>(
				database.GetCollection<UserRating>(nameof(UserRating))));
	}
}