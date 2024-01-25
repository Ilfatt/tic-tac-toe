using System.Reflection;
using Core.Contracts;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class DbConfiguratorAndMigrator
{
	/// <summary>
	///     Создание и настройка подключения к бд
	/// </summary>
	/// <param name="builder">WebApplicationBuilder</param>
	public static void ConfigurePostgresqlConnection(this WebApplicationBuilder builder)
	{
		builder.Services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
			{
				options.Password.RequiredLength = 1;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
			})
			.AddEntityFrameworkStores<AppDbContext>();

		builder.Services.AddDbContext<IDbContext, AppDbContext>(
			options =>
			{
				options.UseNpgsql(
					builder.Configuration.GetConnectionString("PostgreSQL"),
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

	/// <summary>
	///     Миграция бд
	/// </summary>
	/// <param name="host">хост</param>
	public static async Task MigrateDb(this WebApplication host)
	{
		try
		{
			await using var scope = host.Services.CreateAsyncScope();
			var sp = scope.ServiceProvider;

			await using var db = sp.GetRequiredService<AppDbContext>();

			await db.Database.MigrateAsync();
		}
		catch (Exception e)
		{
			host.Logger.LogError(e, "Error while migrating the database");
			Environment.Exit(-1);
		}
	}
}