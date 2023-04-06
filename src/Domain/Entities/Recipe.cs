using Microsoft.EntityFrameworkCore;

namespace Template.Domain.Entities;

public class Recipe : BaseEntity
{
	public string name {get; set;}
	public string subname {get; set;}
	public bool plusRecipe {get; set;}
	public string description {get; set;}
	public int prepTimeMinutes { get; set;}
	public List<Requirement> requirements { get; set; } = new();
	public List<CookingStep> cookingStep { get; set; } = new();
	public List<Season> allSeasons { get; set; } = new();
	public List<Category> allCategories { get; set; } = new();
	public List<PrepDifficulty> allPrepDifficulties { get; set; } = new();
	public List<DietaryPreference> allDietaryPreferences { get; set; } = new();
	public List<Ingredient> allIngredients { get; set; } = new();

	public Recipe()
	{
	}

	public Recipe(
		string name, 
		string subname, 
		bool plusRecipe, 
		string description, 
		int prepTimeMinutes 
	)
	
	{
		this.name = name;
		this.subname = subname;
		this.plusRecipe = plusRecipe;
		this.description = description;
		this.prepTimeMinutes = prepTimeMinutes;
	}
	
	public Recipe(
		string name, 
		string subname, 
		bool plusRecipe, 
		string description, 
		int prepTimeMinutes, 
		List<Requirement> requirements, 
		List<CookingStep> cookingStep, 
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
		this.requirements = requirements;
		this.cookingStep = cookingStep;
		this.allSeasons = allSeasons;
		this.allCategories = allCategories;
		this.allPrepDifficulties = allPrepDifficulties;
		this.allDietaryPreferences = allDietaryPreferences;
		this.allIngredients = allIngredients;
	}
	
}
