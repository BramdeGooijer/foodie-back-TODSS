namespace Template.Domain.Entities;

public class User : BaseEntity
{
	public required string IdentityId { get; set; }
	public required string Name { get; set; }
	public bool IsSubscriber { get; set; }
	public List<Recipe> FavouriteRecipes { get; set; } = new();
	public List<string> DiateryPreferences { get; set; } = new();
}
