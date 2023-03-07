using Microsoft.AspNetCore.Mvc;
using Template.Application.Common.Models;
using Template.Application.Logic.TodoItems.Commands;
using Template.Application.Logic.TodoItems.Queries;

namespace Template.Presentation.Controllers;

public class TodoItemsController : ApiControllerBase
{
	[HttpGet]
	public async Task<ActionResult<PaginatedList<TodoItemBriefDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
	{
		return await Mediator.Send(query);
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create(CreateTodoItemCommand command)
	{
		return await Mediator.Send(command);
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult> Update(Guid id, UpdateTodoItemCommand command)
	{
		if (!id.Equals(command.Id)) return BadRequest();

		await Mediator.Send(command);

		return NoContent();
	}

	[HttpPut("[action]")]
	public async Task<ActionResult> UpdateItemDetails(Guid id, UpdateTodoItemDetailCommand command)
	{
		if (!id.Equals(command.Id)) return BadRequest();

		await Mediator.Send(command);

		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await Mediator.Send(new DeleteTodoItemCommand(id));

		return NoContent();
	}
}
