namespace Template.Domain.Entities;

public class Allergy : BaseEntity
{
	public string typeOfAllergy { get; set; }

	public Allergy()
	{
	}

	public Allergy(string typeOfAllergy)
	{
		this.typeOfAllergy = typeOfAllergy;
	}
}
