namespace Template.Application.Logic.Recipes.Queries;


public record GetRecipeByIdQuery : IRequest<RecipeDto>
{
	public Guid RecipeId { get; init; }
	
	
	internal class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetRecipeByIdHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<RecipeDto> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
		{
			var query = await _context.Recipes
				.Include(recipe => recipe.Ingredients)
				.Include(recipe => recipe.Requirements)
				.Include(recipe => recipe.Seasons)
				.Include(recipe => recipe.CookingStep)
				.Where(recipe => recipe.Id.Equals(request.RecipeId))
				.ProjectTo<RecipeDto>(_mapper.ConfigurationProvider).FirstAsync(cancellationToken);

			return query;
		}
	}
}
