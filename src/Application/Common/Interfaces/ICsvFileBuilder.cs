using Template.Application.Logic.TodoItems.Models;

namespace Template.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
	byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
