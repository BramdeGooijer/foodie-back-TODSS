namespace Template.Domain.Entities;

public class Season : BaseEntity
{
	public string Season { get; set; }
	public List<Recipe> Recipes { get; set; } = new();
	
	public Season(){}

	public Season(string season, List<Recipe> recipes)
	{
		this.Season = season;
		this.Recipes = recipes;
	}
}
