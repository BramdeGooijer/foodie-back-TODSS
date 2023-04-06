using Microsoft.AspNetCore.Mvc;
using Template.Application.Logic.Recipe;
using Template.Domain.Entities;
using Template.Infrastructure.Persistence;

namespace Template.Presentation.Controllers;

public class RecipeController : ApiControllerBase
{
	private readonly RecipeLogic _recipeLogic;

	public RecipeController(ApplicationDbContext context)
	{
		_recipeLogic = new RecipeLogic(context);
	}

	[HttpGet]
	public List<Recipe> getRecipe()
	{
		return _recipeLogic.getAllRecipes();
	}
}
