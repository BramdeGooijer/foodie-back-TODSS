using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Logic.Oauth2.Commands;
using Template.Application.Logic.Oauth2.Models;

namespace Template.Presentation.Controllers;

[Route("OAuth2")]
public class Oauth2Controller : ApiControllerBase
{
	[AllowAnonymous]
	[HttpPost("Token")]
	public async Task<ActionResult<Token>> Create(CreateTokenCommand command)
	{
		return Ok(await Mediator.Send(command));
	}
}
