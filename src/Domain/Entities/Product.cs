namespace Template.Domain.Entities;

public class Product : BaseEntity
{
	public string Name { get; set; }
	public string Amount { get; set; }
	public ICollection<Allergy> AllAllergies { get; set; }

	public Product()
	{
	}

	public Product(string name, string amount, List<Allergy> allAllergies)
	{
		this.Name = name;
		this.Amount = amount;
		this.AllAllergies = allAllergies;
	}
}
