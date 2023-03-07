namespace Template.Application.Dtos;

public class TodoListDto : IMapFrom<TodoList>
{
	public TodoListDto()
	{
		Items = new List<TodoItemDto>();
	}

	public Guid Id { get; set; }

	public string? Title { get; set; }

	public string? Colour { get; set; }

	public IList<TodoItemDto> Items { get; set; }
}
