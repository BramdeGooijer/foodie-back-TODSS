namespace Template.Domain.Entities;

public class Requirement : BaseEntity
{
	public string Name { get; set; }

	public Requirement()
	{
	}

	public Requirement(string name)
	{
		this.Name = name;
	}
}
