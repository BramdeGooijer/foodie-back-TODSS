using System.Globalization;
using CsvHelper.Configuration;
using Template.Application.Logic.TodoItems.Models;

namespace Template.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
	public TodoItemRecordMap()
	{
		AutoMap(CultureInfo.InvariantCulture);

		Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
	}
}
