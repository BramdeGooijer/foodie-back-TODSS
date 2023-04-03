namespace Template.Application.Logic.Category;

using Domain.Entities;

public class CategoryLogic
{
	private readonly IApplicationDbContext _context;

	public CategoryLogic(IApplicationDbContext context)
	{
		_context = context;
	}

	public List<Category> getAllCategories()
	{
		DbSet<Category> categoriesDB = _context.Category;
		List<Category> categories = new();
		foreach (Category perCategory in categoriesDB)
		{
			categories.Add(perCategory); }

		return categories;

	}
}
