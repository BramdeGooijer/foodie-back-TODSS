namespace Template.Application.Dtos;

public class RecipeDto : IMapFrom<Recipe>
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required string SubName { get; set; }
	public bool PlusRecipe { get; set; }
	public required string Description { get; set; }
	public int PrepTimeMinutes { get; set; }
	public List<Requirement> Requirements { get; set; } = new();
	public List<CookingStep> CookingStep { get; set; } = new();
	public List<Season> AllSeasons { get; set; } = new();
	public List<Category> AllCategories { get; set; } = new();
	public List<PrepDifficulty> AllPrepDifficulties { get; set; } = new();
	public List<DietaryPreference> AllDietaryPreferences { get; set; } = new();
	public List<Ingredient> AllIngredients { get; set; } = new();
}
