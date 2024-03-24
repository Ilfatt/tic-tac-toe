using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure;

public interface IAppDbContext
{
	public DbSet<User> Users { get; }
	
	public DbSet<Tag> Tags { get; }
	
	public DbSet<TagToUser> TagToUsers { get; }

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}