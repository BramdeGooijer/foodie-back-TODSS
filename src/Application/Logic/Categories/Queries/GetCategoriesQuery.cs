namespace Template.Application.Logic.Category.Queries;

public record GetCategoriesQuery : IRequest<IList<CategoryDto>>;

internal class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IList<CategoryDto>>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<IList<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
	{
		var categories = await _context.Categories
			.ToListAsync(cancellationToken);

		return _mapper.Map<IList<CategoryDto>>(categories);
	}
}
