namespace Template.Domain.Entities;

public class Ingredient : BaseEntity
{
	public string ingredientName { get; set; }
	public string Amount { get; set; }
	public List<string> allergies { get; set; }
}
