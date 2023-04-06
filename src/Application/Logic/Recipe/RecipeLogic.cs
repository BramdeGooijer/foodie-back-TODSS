namespace Template.Application.Logic.Recipe;

using Domain.Entities;

public class RecipeLogic
{
	private readonly IApplicationDbContext _context;

	public RecipeLogic(IApplicationDbContext context)
	{
		_context = context;
	}

	public List<Recipe> getAllRecipes()
	{
		DbSet<Recipe> recipesDB = _context.Recipes;
		List<Recipe> recipes = new();
		foreach (Recipe perRecipe in recipesDB)
		{
			recipes.Add(perRecipe);
		}

		return recipes;
	}
}
