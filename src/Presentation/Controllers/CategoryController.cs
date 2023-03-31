using Microsoft.AspNetCore.Mvc;
using Template.Application.Common.Interfaces;
using Template.Domain.Entities;

namespace Template.Presentation.Controllers;

public class CategoryController : ApiControllerBase
{
	private readonly IApplicationDbContext _context;

	public CategoryController(IApplicationDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public List<Category> getMockCategories()
	{
		List<Category> allCategoies = new List<Category>();
		foreach (Category perCategory in _context.Category)
		{
			allCategoies.Add(perCategory);
		}

		return allCategoies;
	}
}
