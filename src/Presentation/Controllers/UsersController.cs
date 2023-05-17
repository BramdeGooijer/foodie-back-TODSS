using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Common.Models;
using Template.Application.Dtos;
using Template.Application.Logic.Users.Commands;
using Template.Application.Logic.Users.Queries;

namespace Template.Presentation.Controllers;

public class UsersController : ApiControllerBase
{
	[AllowAnonymous]
	[HttpPost]
	public async Task<ActionResult> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
	{
		await Mediator.Send(command, cancellationToken);
		return NoContent();
	}

	[HttpGet("favoriterecipes")]
	public async Task<ActionResult<PaginatedList<RecipeDto>>> GetFavoritesById([FromQuery] GetFavoritesByIdQuery query, CancellationToken cancellationToken) => 
		Ok(await Mediator.Send(query, cancellationToken));
	}
