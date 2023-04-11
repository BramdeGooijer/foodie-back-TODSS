namespace Template.Domain.Entities;

public class Product : BaseEntity
{
	public required string Name { get; set; }
	public required string Amount { get; set; }
	public List<Allergy> AllAllergies { get; set; } = new();

}
