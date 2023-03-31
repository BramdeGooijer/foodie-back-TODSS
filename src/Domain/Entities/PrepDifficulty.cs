namespace Template.Domain.Entities;

public class PrepDifficulty : BaseEntity
{
	public string name { get; set; }

	public PrepDifficulty()
	{
	}

	public PrepDifficulty(string name)
	{
		this.name = name;
	}
}
