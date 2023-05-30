using ValidationException = Template.Application.Common.Exceptions.ValidationException;

namespace Template.Application.Logic.Recipes.Commands;

public class ToggleFavoriteCommand : IRequest<Unit>
{
	public Guid RecipeId { get; init; }
	public bool state { get; init; }
}

public class ToggleFavoriteCommandValidator : AbstractValidator<ToggleFavoriteCommand>
{
	public ToggleFavoriteCommandValidator()
	{
		RuleFor(command => command.RecipeId)
			.NotEmpty();
	}
}

public class ToggleFavoriteCommandHandler : IRequestHandler<ToggleFavoriteCommand, Unit>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IApplicationDbContext _Context;

	public ToggleFavoriteCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
	{
		_currentUserService = currentUserService;
		_Context = context;
	}

	public async Task<Unit> Handle(ToggleFavoriteCommand request, CancellationToken cancellationToken)
	{
		Guid UserId = new Guid(_currentUserService.UserId ?? throw new InvalidOperationException());

		User user = await _Context.Users
			.Include(user => user.FavouriteRecipes)
			.FirstAsync(user => user.Id.Equals(UserId));
		Console.WriteLine(user.Id);

		if (request.state.Equals(false))
		{
			user = RemoveFavoriteFromUser(request, user);
		}
		else
		{
			user = await AddFavoriteToUser(request, user);
		}

		_Context.Users.Update(user);
		await _Context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}

	public User RemoveFavoriteFromUser(ToggleFavoriteCommand request, User user)
	{
		Console.WriteLine("remove");
		Console.WriteLine(request.RecipeId);
		Console.WriteLine("ids");
		foreach (var perRecipe in user.FavouriteRecipes)
		{
			Console.WriteLine(perRecipe.Id);
		}
		try
		{
			Recipe recipe = user.FavouriteRecipes.First(recipe => recipe.Id.Equals(request.RecipeId));
			Console.WriteLine("remove1");

			user.FavouriteRecipes.Remove(recipe);
			return user;
		}
		catch (InvalidOperationException e)
		{
			throw new ValidationException(nameof(request.RecipeId), "User doesnt have a recipe favoured with this recipeId!");
		}

		
	}
	
	public async Task<User> AddFavoriteToUser(ToggleFavoriteCommand request, User user)
	{
		try
		{
			Console.WriteLine("add");
		Recipe recipe = await _Context.Recipes
			.Include(recipe => recipe.Ingredients)
			.Include(recipe => recipe.Requirements)
			.Include(recipe => recipe.Seasons)
			.Include(recipe => recipe.CookingSteps)
			.FirstAsync(recipe => recipe.Id.Equals(request.RecipeId));
		Console.WriteLine("give userid");
		Console.WriteLine(user.Id);
		user.FavouriteRecipes.Add(recipe);
		return user;
		} catch (InvalidOperationException e)
		{
			throw new ValidationException(nameof(request.RecipeId), "No recipe found with this recipeId");
		}
	
	}
}
