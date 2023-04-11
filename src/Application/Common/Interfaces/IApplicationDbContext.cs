namespace Template.Application.Common.Interfaces;

public interface IApplicationDbContext
{
	DbSet<TodoList> TodoLists { get; }

	DbSet<TodoItem> TodoItems { get; }

	DbSet<User> Users { get; }
	
	DbSet<Recipe> Recipes { get; }
	

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
