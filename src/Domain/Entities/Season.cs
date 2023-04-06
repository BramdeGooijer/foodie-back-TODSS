namespace Template.Domain.Entities;

public class Season : BaseEntity
{
	public string season { get; set; }
	public List<Recipe> recipes { get; set; } = new();
	
	public Season(){}

	public Season(string season, List<Recipe> recipes)
	{
		this.season = season;
		this.recipes = recipes;
	}
}
