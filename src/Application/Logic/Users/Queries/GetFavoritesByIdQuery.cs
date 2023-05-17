namespace Template.Application.Logic.Users.Queries;

public record GetFavoritesByIdQuery : IRequest<PaginatedList<RecipeDto>>
{
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

	public Task<PaginatedList<RecipeDto>> Handle(GetFavoritesByIdQuery request, CancellationToken cancellationToken)
	{
		var configuration = new Mapper
		
		List<Recipe> recipes = _context.Users
			.Where(user => user.Id.Equals(request.UserId))
			.Include(user => user.FavouriteRecipes)
			.pro
		return null;
	}
	
}
