namespace Template.Domain.Entities;

public class Allergy : BaseEntity
{
	public string TypeOfAllergy { get; set; }

	public Allergy()
	{
	}

	public Allergy(string typeOfAllergy)
	{
		this.TypeOfAllergy = typeOfAllergy;
	}
}
