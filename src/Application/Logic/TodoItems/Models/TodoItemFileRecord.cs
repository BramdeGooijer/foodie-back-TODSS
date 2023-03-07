namespace Template.Application.Logic.TodoItems.Models;

public class TodoItemRecord : IMapFrom<TodoItem>
{
	public string? Title { get; set; }

	public bool Done { get; set; }
}
