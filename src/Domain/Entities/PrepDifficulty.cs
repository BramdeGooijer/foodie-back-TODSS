namespace Template.Domain.Entities;

public class PrepDifficulty : BaseEntity
{
	public string Name { get; set; }
	public List<Recipe> Recipes { get; set; } = new();

	public PrepDifficulty()
	{
	}

	public PrepDifficulty(string name, List<Recipe> recipes)
	{
		this.Name = name;
		this.Recipes = recipes;
	}
}
