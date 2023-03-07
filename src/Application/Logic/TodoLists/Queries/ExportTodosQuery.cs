using Template.Application.Logic.TodoItems.Models;

namespace Template.Application.Logic.TodoLists.Queries;

public record ExportTodosQuery : IRequest<ExportTodosVm>
{
	public Guid ListId { get; init; }
}

public class ExportTodosQueryHandler : IRequestHandler<ExportTodosQuery, ExportTodosVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly ICsvFileBuilder _fileBuilder;

	public ExportTodosQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
	{
		_context = context;
		_mapper = mapper;
		_fileBuilder = fileBuilder;
	}

	public async Task<ExportTodosVm> Handle(ExportTodosQuery request, CancellationToken cancellationToken)
	{
		var records = await _context.TodoItems
				.Where(todoItem => todoItem.ListId.Equals(request.ListId))
				.ProjectTo<TodoItemRecord>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

		var vm = new ExportTodosVm(
			"TodoItems.csv",
			"text/csv",
			_fileBuilder.BuildTodoItemsFile(records));

		return vm;
	}
}

public class ExportTodosVm
{
	public ExportTodosVm(string fileName, string contentType, byte[] content)
	{
		FileName = fileName;
		ContentType = contentType;
		Content = content;
	}

	public string FileName { get; set; }

	public string ContentType { get; set; }

	public byte[] Content { get; set; }
}
