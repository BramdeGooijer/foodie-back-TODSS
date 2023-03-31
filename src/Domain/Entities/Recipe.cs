namespace Template.Domain.Entities;

public class Recipe : BaseEntity
{
	public required string name {get; set;}
	public required string subname {get; set;}
	public bool plusRecipe {get; set;}
	public required string description {get; set;}
	public int prepTimeMinutes { get; set;}
	public Requirement requirement { get; set; }
	public CookingStep cookingStep { get; set; }
	public List<Season> allSeasons { get; set; }
	public ICollection<Category> allCategories { get; set; }
	public ICollection<PrepDifficulty> allPrepDifficulties { get; set; }
	public ICollection<DietaryPreference> allDietaryPreferences { get; set; }
	public ICollection<Ingredient> allIngredients { get; set; }

	public Recipe()
	{
	}

	public Recipe(
		string name, 
		string subname, 
		bool plusRecipe, 
		string description, 
		int prepTimeMinutes, 
		Requirement requirement, 
		CookingStep cookingStep, 
		List<Season> allSeasons, 
		List<Category> allCategories, 
		List<PrepDifficulty> allPrepDifficulties, 
		List<DietaryPreference> allDietaryPreferences, 
		List<Ingredient> allIngredients
		)
	{
		this.name = name;
		this.subname = subname;
		this.plusRecipe = plusRecipe;
		this.description = description;
		this.prepTimeMinutes = prepTimeMinutes;
		this.requirement = requirement;
		this.cookingStep = cookingStep;
		this.allSeasons = allSeasons;
		this.allCategories = allCategories;
		this.allPrepDifficulties = allPrepDifficulties;
		this.allDietaryPreferences = allDietaryPreferences;
		this.allIngredients = allIngredients;
	}
}
