using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Common.Models;
using Template.Application.Dtos;
using Template.Application.Logic.Users.Commands;

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

	// [HttpGet]
	// public async Task<ActionResult<PaginatedList<RecipeDto>> GetFavoritesById()
	// {
	// 	
	// }
}
