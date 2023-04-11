namespace Template.Domain.Entities;

public class CookingStep : BaseEntity
{
	public string Description { get; set; }

	public CookingStep()
	{
	}

	public CookingStep(string description)
	{
		this.Description = description;
	}
}
