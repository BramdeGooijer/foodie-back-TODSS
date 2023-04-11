namespace Template.Domain.Entities;

public class User : BaseEntity
{
	public required string IdentityId { get; set; }
	public string Name { get; set; }
	public bool IsSubscriber { get; set; }
	public List<Recipe> FavouriteRecipes { get; set; }
	public List<DietaryPreference> AllDietaryPreferences { get; set; }

	public User()
	{
	}

	public User(string identityId, string name, bool isSubscriber, List<Recipe> favouriteRecipes, List<DietaryPreference> allDietaryPreferences)
	{
		this.IdentityId = identityId;
		this.Name = name;
		this.IsSubscriber = isSubscriber;
		this.FavouriteRecipes = favouriteRecipes;
		this.AllDietaryPreferences = allDietaryPreferences;
	}
}
