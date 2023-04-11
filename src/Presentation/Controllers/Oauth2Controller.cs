using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Logic.Oauth2.Commands;
using Template.Application.Logic.Oauth2.Models;

namespace Template.Presentation.Controllers;

[Route("oauth2")]
public class Oauth2Controller : ApiControllerBase
{
	[AllowAnonymous]
	[HttpPost("token")]
	public async Task<ActionResult<Token>> Create(CreateTokenCommand command)
	{
		return Ok(await Mediator.Send(command));
	}
}
