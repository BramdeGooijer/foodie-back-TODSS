namespace Template.Domain.Entities;

public class PrepDifficulty : BaseEntity
{
	public string name { get; set; }
	public List<Recipe> recipes { get; set; } = new();

	public PrepDifficulty()
	{
	}

	public PrepDifficulty(string name, List<Recipe> recipes)
	{
		this.name = name;
		this.recipes = recipes;
	}
}
