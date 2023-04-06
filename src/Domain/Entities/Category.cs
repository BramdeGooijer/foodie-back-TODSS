namespace Template.Domain.Entities;

public class Category : BaseEntity
{
	public string name { get; set; }
	public List<Recipe> recipes { get; set; } = new();
	public Category()
	{
	}

	public Category(string name, List<Recipe> recipes)
	{
		this.name = name;
		this.recipes = recipes;
	}
}
