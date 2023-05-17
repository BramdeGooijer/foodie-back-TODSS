using Microsoft.AspNetCore.Mvc;
using Template.Application.Common.Models;
using Template.Application.Dtos;
using Template.Application.Logic.Recipes.Queries;
using Template.Application.Logic.Users.Queries;

namespace Template.Presentation.Controllers;

public class RecipesController : ApiControllerBase
{
	[HttpGet]
	public async Task<ActionResult<PaginatedList<RecipeDto>>> GetRecipe([FromQuery] GetRecipesQuery query, CancellationToken cancellationToken) =>
		Ok(await Mediator.Send(query, cancellationToken));

	[HttpGet("{id:guid}")]
	public async Task<RecipeDto> getRecipeById(Guid id) =>
		await Mediator.Send(new GetRecipeByIdQuery
		{
			RecipeId = id
		});
	
	[HttpGet("favorite")]
	public async Task<ActionResult<PaginatedList<RecipeDto>>> GetFavoritesById([FromQuery] GetFavoritesByIdQuery query, CancellationToken cancellationToken) => 
		Ok(await Mediator.Send(query, cancellationToken));
}

