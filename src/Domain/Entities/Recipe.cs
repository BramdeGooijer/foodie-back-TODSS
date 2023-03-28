namespace Template.Domain.Entities;

public class Recipe : BaseEntity
{
	public required string name {get; set;}
	public required string subname {get; set;}
	public bool plusRecipe {get; set;}
	public required string description {get; set;}
	public int prepTimeMinutes { get; set;}
	
}
