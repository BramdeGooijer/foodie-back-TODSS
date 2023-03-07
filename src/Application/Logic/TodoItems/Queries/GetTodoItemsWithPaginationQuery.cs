namespace Template.Application.Logic.TodoItems.Queries;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
	public Guid ListId { get; init; }

	public int PageNumber { get; init; } = 1;

	public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryValidator : AbstractValidator<GetTodoItemsWithPaginationQuery>
{
	public GetTodoItemsWithPaginationQueryValidator()
	{
		RuleFor(query => query.ListId)
			.NotEmpty().WithMessage("ListId is required.");

		RuleFor(query => query.PageNumber)
			.GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

		RuleFor(query => query.PageSize)
			.GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
	}
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
	{
		return await _context.TodoItems
			.Where(todoItem => todoItem.ListId.Equals(request.ListId))
			.OrderBy(todoItem => todoItem.Title)
			.MapToPaginatedListAsync<TodoItem, TodoItemBriefDto>(_mapper.ConfigurationProvider, request.PageNumber, request.PageSize, cancellationToken);
	}
}

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
	public Guid Id { get; set; }

	public Guid ListId { get; set; }

	public string? Title { get; set; }

	public bool Done { get; set; }
}
