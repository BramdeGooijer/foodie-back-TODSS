namespace Template.Domain.Entities;

public class DietaryPreference : BaseEntity
{
	public string name { get; set; }
	public List<Recipe> recipes { get; set; } = new();

	public DietaryPreference()
	{
	}

	public DietaryPreference(string name, List<Recipe> recipes)
	{
		this.name = name;
		this.recipes = recipes;
	}
}
