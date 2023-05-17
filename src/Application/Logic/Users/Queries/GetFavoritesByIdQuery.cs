namespace Template.Application.Logic.Users.Queries;

public record GetFavoritesByIdQuery : IRequest<PaginatedList<RecipeDto>>
{
	public int PageNumber { get; init; }
	public int PageSize { get; init; }
	public Guid UserId { get; init; }
}

public class GetFavoritesByIdValidator : AbstractValidator<GetFavoritesByIdQuery>
{
	public GetFavoritesByIdValidator()
	{
		RuleFor(command => command.UserId)
			.NotEmpty();
	}
}

public class GetFavoritesByIdHandlerToken : IRequestHandler<GetFavoritesByIdQuery, PaginatedList<RecipeDto>>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetFavoritesByIdHandlerToken(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<PaginatedList<RecipeDto>> Handle(GetFavoritesByIdQuery request, CancellationToken cancellationToken)
	{

		User? users = await _context.Users
			.Include(user => user.FavouriteRecipes)
			.Where(user => user.Id.Equals(request.UserId))
			.FirstAsync(cancellationToken);
		List<RecipeDto> recipeDtos = _mapper.Map<List<RecipeDto>>(users.FavouriteRecipes);
		PaginatedList<RecipeDto> recipeDtosPaginatedList =
			new PaginatedList<RecipeDto>(recipeDtos, recipeDtos.Count, request.PageNumber, request.PageSize);

		return recipeDtosPaginatedList;
	}
	
}
