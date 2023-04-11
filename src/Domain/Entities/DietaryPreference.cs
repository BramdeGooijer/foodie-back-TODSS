namespace Template.Domain.Entities;

public class DietaryPreference : BaseEntity
{
	public string Name { get; set; }
	public List<Recipe> Recipes { get; set; } = new();

	public DietaryPreference()
	{
	}

	public DietaryPreference(string name, List<Recipe> recipes)
	{
		this.Name = name;
		this.Recipes = recipes;
	}
}
