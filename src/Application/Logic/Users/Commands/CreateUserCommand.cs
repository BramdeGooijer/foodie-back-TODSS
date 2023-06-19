
using ValidationException = Template.Application.Common.Exceptions.ValidationException;

namespace Template.Application.Logic.Users.Commands;

public class CreateUserCommand : IRequest<Unit>
{
	public required string Name { get; init; }
	public required string UserName { get; init; }
	public required string Password { get; init; }

}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator(IIdentityService identityService)
	{
		RuleFor(command => command.UserName)
			.NotEmpty()
			.EmailAddress()
			.MustAsync(async (username, _) => await identityService.GetUserIdAsync(username) == null)
			.WithMessage("Email is already in use.");

		RuleFor(command => command.Password)
			.NotEmpty();
		
		RuleFor(command => command.Name)
			.NotEmpty();
	}
	
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
	private readonly IIdentityService _identityService;
	private readonly IApplicationDbContext _context;

	public CreateUserCommandHandler(IIdentityService identityService, IApplicationDbContext context)
	{
		_identityService = identityService;
		_context = context;
	}

	public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		(Result result, string userId) = await _identityService.CreateUserAsync(request.UserName, request.Password);
		
		if (!result.Succeeded)
			throw new ValidationException(nameof(request.UserName), "Unexpected error.");

		_context.Users.Add(new User
		{
			Id = new Guid(userId),
			IdentityId = userId,
			Name = request.Name,
		});
		await _context.SaveChangesAsync(cancellationToken);
		
		return Unit.Value;
	}
	
}
