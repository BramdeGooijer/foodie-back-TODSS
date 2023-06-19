using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
	
	[HttpPost("changename")]
	public async Task<ActionResult> ChangeName(ChangeNameCommand command, CancellationToken cancellationToken)
	{
		await Mediator.Send(command, cancellationToken);
		return NoContent();
	}
}
	
