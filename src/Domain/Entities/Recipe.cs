namespace Template.Domain.Entities;

public class Recipe : BaseEntity
{
	public required string Name { get; set; }
	public required string SubName { get; set; }
	public required bool PlusRecipe { get; set; }
	public required string Description { get; set; }
	public required int PrepTimeMinutes { get; set; }
	public List<Requirement> Requirements { get; set; } = new();
	public List<CookingStep> CookingStep { get; set; } = new();
	public List<Season> Seasons { get; set; } = new();
	public List<Category> Categories { get; set; } = new();
	public List<PrepDifficulty> PrepDifficulties { get; set; } = new();
	public List<DietaryPreference> DietaryPreferences { get; set; } = new();
	public List<Ingredient> Ingredients { get; set; } = new();
}
