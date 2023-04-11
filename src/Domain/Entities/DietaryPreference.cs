namespace Template.Domain.Entities;

public class DietaryPreference : BaseEntity
{
	public required string Name { get; set; }
	public List<Recipe> Recipes { get; set; } = new();
	
}
