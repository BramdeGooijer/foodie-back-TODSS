namespace Template.Application.Logic.Recipes.Queries;

public record GetRecipesQuery : IRequest<PaginatedList<RecipeDto>>
{
	public int? PageNumber { get; init; }
	public int? PageSize { get; init; }
	public string? CategoryName { get; init; }
	public string? RecipeName { get; init; }
}

public class GetRecipesQueryValidator : AbstractValidator<GetRecipesQuery>
{
	public GetRecipesQueryValidator(IApplicationDbContext context)
	{
	}
}

internal class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, PaginatedList<RecipeDto>>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetRecipesQueryHandler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<PaginatedList<RecipeDto>> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
	{
		IQueryable<Recipe> query = _context.Recipes
			.Include(recipe => recipe.Ingredients)
			.Include(recipe => recipe.Requirements)
			.Include(recipe => recipe.Seasons)
			.Include(recipe => recipe.CookingSteps)
			.OrderBy(recipe => recipe.Name)
			.AsQueryable();

		if (request.CategoryName != null)
		{
			query = query.Where(r => r.Categories.Any(c => c == request.CategoryName));
		}

		if (request.RecipeName != null)
		{
			query = query.Where(r => r.Name.ToLower().StartsWith(request.RecipeName.ToLower()));
		}

		return await query.MapToPaginatedListAsync<Recipe, RecipeDto>(_mapper.ConfigurationProvider, request.PageNumber, request.PageSize,
			cancellationToken);
	}
}
