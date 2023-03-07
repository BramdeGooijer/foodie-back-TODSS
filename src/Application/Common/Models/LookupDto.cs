namespace Template.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes and used for a test case.
public class LookupDto : IMapFrom<TodoList>, IMapFrom<TodoItem>
{
	public int Id { get; set; }

	public string? Title { get; set; }
}
