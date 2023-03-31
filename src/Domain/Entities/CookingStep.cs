namespace Template.Domain.Entities;

public class CookingStep : BaseEntity
{
	public string description { get; set; }

	public CookingStep()
	{
	}

	public CookingStep(string description)
	{
		this.description = description;
	}
}
