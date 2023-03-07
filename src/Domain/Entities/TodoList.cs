namespace Template.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public ICollection<TodoItem> Items { get; } = new List<TodoItem>();
}
