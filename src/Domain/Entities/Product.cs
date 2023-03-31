namespace Template.Domain.Entities;

public class Product : BaseEntity
{
	public string name { get; set; }
	public string amount { get; set; }
	public ICollection<Allergy> allAllergies { get; set; }

	public Product()
	{
	}

	public Product(string name, string amount, List<Allergy> allAllergies)
	{
		this.name = name;
		this.amount = amount;
		this.allAllergies = allAllergies;
	}
}
