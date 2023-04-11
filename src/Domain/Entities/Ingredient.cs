using Microsoft.EntityFrameworkCore;

namespace Template.Domain.Entities;

public class Ingredient : BaseEntity
{
	public string Amount { get; set; }
	public Product Product { get; set; }
	
	public List<Recipe> Recipes { get; set; } = new();

}
