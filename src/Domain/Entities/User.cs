namespace Template.Domain.Entities;

public class User : BaseEntity
{
	public required string IdentityId { get; set; }
	public required string Name { get; set; }
	public bool IsSubscriber { get; set; } = false;
	public IList<Recipe> FavouriteRecipes { get; set; } = new List<Recipe>();
	public List<string> DiateryPreferences { get; set; } = new();
}
