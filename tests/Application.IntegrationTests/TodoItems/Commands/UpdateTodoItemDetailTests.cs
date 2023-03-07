using FluentAssertions;
using NUnit.Framework;
using Template.Application.Common.Exceptions;
using Template.Application.Logic.TodoItems.Commands;
using Template.Application.Logic.TodoLists.Commands;
using Template.Domain.Entities;
using Template.Domain.Enums;

namespace Template.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class UpdateTodoItemDetailTests : BaseTestFixture
{
	[Test]
	public async Task ShouldRequireValidTodoItemId()
	{
		var command = new UpdateTodoItemCommand { Id = new Guid("f71065d0-3c5a-4dfd-80ed-8e436ecda1ae"), Title = "New Title" };
		await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
	}

	[Test]
	public async Task ShouldUpdateTodoItem()
	{
		var userId = await RunAsDefaultUserAsync();

		var listId = await SendAsync(new CreateTodoListCommand
		{
			Title = "New List"
		});

		var itemId = await SendAsync(new CreateTodoItemCommand
		{
			ListId = listId,
			Title = "New Item"
		});

		var command = new UpdateTodoItemDetailCommand
		{
			Id = itemId,
			ListId = listId,
			Note = "This is the note.",
			Priority = PriorityLevel.High
		};

		await SendAsync(command);

		var item = await FindAsync<TodoItem>(itemId);

		item.Should().NotBeNull();
		item!.ListId.Should().Be(command.ListId);
		item.Note.Should().Be(command.Note);
		item.Priority.Should().Be(command.Priority);
		item.LastModifiedBy.Should().NotBeNull();
		item.LastModifiedBy.Should().Be(userId);
		item.LastModified.Should().NotBeNull();
		item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
	}
}
