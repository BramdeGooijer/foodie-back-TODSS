using Microsoft.EntityFrameworkCore;

namespace Template.Domain.Entities;

public class Ingredient : BaseEntity
{
	public string amount { get; set; }
	public Product product { get; set; }
	
	public List<Recipe> recipes { get; set; } = new();

	public Ingredient()
	{
	}

	public Ingredient(string amount, Product product, List<Recipe> recipes)
	{
		this.amount = amount;
		this.product = product;
		this.recipes = recipes;
	}
}
