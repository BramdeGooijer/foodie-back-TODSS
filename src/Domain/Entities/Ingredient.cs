using Microsoft.EntityFrameworkCore;

namespace Template.Domain.Entities;

public class Ingredient : BaseEntity
{
	public string amount { get; set; }
	public Product product { get; set; }

	public Ingredient()
	{
	}

	public Ingredient(string amount, Product product)
	{
		this.amount = amount;
		this.product = product;
	}
}
