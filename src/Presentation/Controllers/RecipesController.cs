using Microsoft.AspNetCore.Mvc;
using Template.Application.Common.Models;
using Template.Application.Dtos;
using Template.Application.Logic.Recipes.Queries;

namespace Template.Presentation.Controllers;

public class RecipesController : ApiControllerBase
{
	[HttpGet]
	public async Task<ActionResult<PaginatedList<RecipeDto>>> GetRecipe([FromQuery] GetRecipesQuery query, CancellationToken cancellationToken)
	{
		return Ok(await Mediator.Send(query, cancellationToken));
	}

	[HttpGet("{id:guid}")]
	public async Task<RecipeDto> getRecipeById(Guid id)
	{
		return await Mediator.Send(new GetRecipeByIdQuery()
		{
			RecipeId = id
		});
	}

}
