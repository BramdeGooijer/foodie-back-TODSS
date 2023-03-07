namespace Template.Application.Logic.TodoLists.Queries;

public record GetTodosQuery : IRequest<TodosVm>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
	{
		return new TodosVm
		{
			PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
				.Cast<PriorityLevel>()
				.Select(priorityLevel => new PriorityLevelDto
				{
					Name = priorityLevel.ToString()
				})
				.ToList(),

			Lists = await _context.TodoLists
				.AsNoTracking()
				.Include(tl => tl.Items)
				.OrderBy(dto => dto.Title)
				.MapToListAsync<TodoList, TodoListDto>(_mapper.ConfigurationProvider, cancellationToken)
		};
	}
}

public record TodosVm
{
	public IList<PriorityLevelDto> PriorityLevels { get; set; } = new List<PriorityLevelDto>();

	public IList<TodoListDto> Lists { get; set; } = new List<TodoListDto>();
}
