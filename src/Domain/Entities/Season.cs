namespace Template.Domain.Entities;

public class Season : BaseEntity
{
	public required string SeasonName { get; set; }
	public List<Recipe> Recipes { get; set; } = new();
}
