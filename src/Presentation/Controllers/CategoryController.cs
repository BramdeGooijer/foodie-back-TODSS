using Microsoft.AspNetCore.Mvc;
using Template.Application.Common.Interfaces;
using Template.Application.Logic.Category;
using Template.Domain.Entities;
using Template.Infrastructure.Persistence;

namespace Template.Presentation.Controllers;

public class CategoryController : ApiControllerBase
{
	private readonly ApplicationDbContext _context;
	private readonly CategoryLogic _categoryLogic;

	public CategoryController(ApplicationDbContext context)
	{
		_context = context;
		_categoryLogic = new CategoryLogic(context);
	}

	[HttpGet]
	public List<Category> getMockCategories()
	{
		return _categoryLogic.getAllCategories();
	}
}
