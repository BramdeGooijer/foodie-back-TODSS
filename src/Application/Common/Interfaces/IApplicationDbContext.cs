namespace Template.Application.Common.Interfaces;

public interface IApplicationDbContext
{
	DbSet<User> Users { get; }

	DbSet<Recipe> Recipes { get; }


	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
