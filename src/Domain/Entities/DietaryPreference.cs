namespace Template.Domain.Entities;

public class DietaryPreference : BaseEntity
{
	public string name { get; set; }

	public DietaryPreference()
	{
	}

	public DietaryPreference(string name)
	{
		this.name = name;
	}
}
