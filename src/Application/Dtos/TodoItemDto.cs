namespace Template.Application.Dtos;

public class TodoItemDto : IMapFrom<TodoItem>
{
	public Guid Id { get; set; }

	public Guid ListId { get; set; }

	public string? Title { get; set; }

	public bool Done { get; set; }

	public PriorityLevel Priority { get; set; }

	public string? Note { get; set; }
}
