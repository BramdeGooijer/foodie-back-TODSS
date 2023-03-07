using FluentAssertions;
using NUnit.Framework;
using Template.Application.Common.Exceptions;
using Template.Application.Logic.TodoLists.Commands;
using Template.Domain.Entities;

namespace Template.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : BaseTestFixture
{
	[Test]
	public async Task ShouldRequireValidTodoListId()
	{
		var command = new DeleteTodoListCommand(new Guid("f71065d0-3c5a-4dfd-80ed-8e436ecda1ae"));
		await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
	}

	[Test]
	public async Task ShouldDeleteTodoList()
	{
		var listId = await SendAsync(new CreateTodoListCommand
		{
			Title = "New List"
		});

		await SendAsync(new DeleteTodoListCommand(listId));

		var list = await FindAsync<TodoList>(listId);

		list.Should().BeNull();
	}
}
