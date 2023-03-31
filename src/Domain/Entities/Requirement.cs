namespace Template.Domain.Entities;

public class Requirement : BaseEntity
{
	public string name { get; set; }

	public Requirement()
	{
	}

	public Requirement(string name)
	{
		this.name = name;
	}
}
