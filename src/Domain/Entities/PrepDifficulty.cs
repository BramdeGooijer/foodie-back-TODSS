namespace Template.Domain.Entities;

public class PrepDifficulty : BaseEntity
{
	public required string Name { get; set; }
	public List<Recipe> Recipes { get; set; } = new();
	
}
