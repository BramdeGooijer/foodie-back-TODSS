using System.Globalization;
using CsvHelper;
using Template.Application.Common.Interfaces;
using Template.Application.Logic.TodoItems.Models;
using Template.Infrastructure.Files.Maps;

namespace Template.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
	public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
	{
		using var memoryStream = new MemoryStream();
		using (var streamWriter = new StreamWriter(memoryStream))
		{
			using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

			csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
			csvWriter.WriteRecords(records);
		}

		return memoryStream.ToArray();
	}
}