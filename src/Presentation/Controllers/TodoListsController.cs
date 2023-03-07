using Microsoft.AspNetCore.Mvc;
using Template.Application.Logic.TodoLists.Commands;
using Template.Application.Logic.TodoLists.Queries;

namespace Template.Presentation.Controllers;

public class TodoListsController : ApiControllerBase
{
	[HttpGet]
	public async Task<ActionResult<TodosVm>> Get()
	{
		return Ok(await Mediator.Send(new GetTodosQuery()));
	}

	[HttpGet("{id:guid}")]
	public async Task<FileResult> Get(Guid id)
	{
		var vm = await Mediator.Send(new ExportTodosQuery
		{
			ListId = id
		});

		return File(vm.Content, vm.ContentType, vm.FileName);
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create(CreateTodoListCommand command)
	{
		return Ok(await Mediator.Send(command));
	}

	[HttpPut("{id:guid}")]
	public async Task<ActionResult> Update(Guid id, UpdateTodoListCommand command)
	{
		if (!id.Equals(command.Id)) return BadRequest();

		await Mediator.Send(command);

		return NoContent();
	}

	[HttpDelete("{id:guid}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await Mediator.Send(new DeleteTodoListCommand(id));

		return NoContent();
	}
}
