using Microsoft.EntityFrameworkCore;
using Model;

namespace Core.Contracts;

public interface IDbContext
{
	public DbSet<Game> Games { get; set; }

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}