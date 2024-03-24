using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class AppDbContext : DbContext, IAppDbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
	{
	}

	public DbSet<User> Users { get; init; } = default!;
	
	public DbSet<Tag> Tags { get; init; } = default!;
	
	public DbSet<TagToUser> TagToUsers { get; init; } = default!;
}