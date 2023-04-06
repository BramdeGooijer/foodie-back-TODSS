namespace Template.Domain.Entities;

public class Category : BaseEntity
{
	public string name { get; set; }
	public List<Recipe> recipes { get; set; } = new();
	public Category()
	{
	}

	public Category(string name)
	{
		this.name = name;
	}
}
