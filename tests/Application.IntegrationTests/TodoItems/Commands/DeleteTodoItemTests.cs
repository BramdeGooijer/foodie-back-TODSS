using FluentAssertions;
using NUnit.Framework;
using Template.Application.Common.Exceptions;
using Template.Application.Logic.TodoItems.Commands;
using Template.Application.Logic.TodoLists.Commands;
using Template.Domain.Entities;

namespace Template.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class DeleteTodoItemTests : BaseTestFixture
{
	[Test]
	public async Task ShouldRequireValidTodoItemId()
	{
		var command = new DeleteTodoItemCommand(new Guid("9916d80b-cbed-425e-92d0-c8e3bb12d0d2"));

		await FluentActions.Invoking(() =>
			SendAsync(command)).Should().ThrowAsync<NotFoundException>();
	}

	[Test]
	public async Task ShouldDeleteTodoItem()
	{
		var listId = await SendAsync(new CreateTodoListCommand
		{
			Title = "New List"
		});

		var itemId = await SendAsync(new CreateTodoItemCommand
		{
			ListId = listId,
			Title = "New Item"
		});

		await SendAsync(new DeleteTodoItemCommand(itemId));

		var item = await FindAsync<TodoItem>(itemId);

		item.Should().BeNull();
	}
}
