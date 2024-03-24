using System.Reflection;
using Asp.Net;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions;

public static class DbConfiguratorAndMigrator
{
	public static void ConfigurePostgresqlConnection(this WebAppBuilder builder)
	{
		builder.ServiceCollection.AddDbContext<AppDbContext>(
			options =>
			{
				options.UseNpgsql(
					builder.Configuration["ConnectionStrings:PostreSQL"],
					opt =>
					{
						opt.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name);
						opt.EnableRetryOnFailure(
							15,
							TimeSpan.FromSeconds(30),
							null);
					});
			});
	}

	public static async Task MigrateDb(this IServiceProvider serviceProvider)
	{
		await using var scope = serviceProvider.CreateAsyncScope();
		var sp = scope.ServiceProvider;

		await using var db = sp.GetRequiredService<AppDbContext>();

		await db.Database.MigrateAsync();
	}
}