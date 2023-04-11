using Microsoft.AspNetCore.Mvc;
using Template.Application.Dtos;
using Template.Application.Logic.Category.Queries;

namespace Template.Presentation.Controllers;

public class CategoriesController : ApiControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IList<CategoryDto>>> GetCategories(CancellationToken cancellationToken)
	{
		return Ok(await Mediator.Send(new GetCategoriesQuery(), cancellationToken));
	}
}
