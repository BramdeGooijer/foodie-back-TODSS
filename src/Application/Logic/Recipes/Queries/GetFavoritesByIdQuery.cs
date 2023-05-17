namespace Template.Application.Logic.Recipes.Queries;

public record GetFavoritesByIdQuery : IRequest<PaginatedList<RecipeDto>>
{
	public int? PageNumber { get; init; }
	public int? PageSize { get; init; }
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

		User users = await _context.Users
			.Include(user => user.FavouriteRecipes)
			.Where(user => user.Id.Equals(request.UserId))
			.FirstAsync(cancellationToken);

		List<Guid> recipeIds = new List<Guid>();
		foreach (var perRecipe in users.FavouriteRecipes)
		{
			recipeIds.Add((perRecipe.Id));
		}

		PaginatedList<RecipeDto> recipes = await _context.Recipes
			.Include(recipe => recipe.Ingredients)
			.Include(recipe => recipe.Requirements)
			.Include(recipe => recipe.Seasons)
			.Include(recipe => recipe.CookingSteps)
			.Where(recipe => recipeIds.Contains(recipe.Id))
			.MapToPaginatedListAsync<Recipe, RecipeDto>(_mapper.ConfigurationProvider, request.PageNumber, request.PageSize, cancellationToken);
		return recipes;

	}
	
}
