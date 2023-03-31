namespace Template.Domain.Entities;

public class User : BaseEntity
{
	public required string IdentityId { get; set; }
	public string name { get; set; }
	public bool isSubscriber { get; set; }
	public List<Recipe> favouriteRecipes { get; set; }
	public List<DietaryPreference> allDietaryPreferences { get; set; }

	public User()
	{
	}

	public User(string IdentityId, string name, bool isSubscriber, List<Recipe> favouriteRecipes, List<DietaryPreference> allDietaryPreferences)
	{
		this.IdentityId = IdentityId;
		this.name = name;
		this.isSubscriber = isSubscriber;
		this.favouriteRecipes = favouriteRecipes;
		this.allDietaryPreferences = allDietaryPreferences;
	}
}
