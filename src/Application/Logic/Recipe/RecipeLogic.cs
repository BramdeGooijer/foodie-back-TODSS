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
		List<Recipe> recipesDB = _context.Recipes
			.Include(recipe => recipe.allCategories)
			.Include(recipe => recipe.allIngredients)
			.Include(recipe => recipe.requirements)
			.Include(recipe => recipe.allSeasons)
			.Include(recipe => recipe.cookingStep)
			.Include(recipe => recipe.allDietaryPreferences)
			.Include(recipe => recipe.allPrepDifficulties)
			.ToList();

		return recipesDB;
	}
}
