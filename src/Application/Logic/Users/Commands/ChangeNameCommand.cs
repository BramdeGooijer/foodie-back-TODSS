namespace Template.Application.Logic.Users.Commands;

public class ChangeNameCommand : IRequest<Unit>
{
	public required string newName { get; init; }

	public class ChangeNameCommandValidator : AbstractValidator<ChangeNameCommand>
	{
		public ChangeNameCommandValidator()
		{
			RuleFor(command => command.newName)
				.NotEmpty();
		}

		public class ChangeNameCommandHandler : IRequestHandler<ChangeNameCommand, Unit>
		{
			private readonly ICurrentUserService _currentUserService;
			private readonly IApplicationDbContext _context;

			public ChangeNameCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
			{
				_context = context;
				_currentUserService = currentUserService;
			}

			public async Task<Unit> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
			{
				
				Guid UserId = new Guid(_currentUserService.UserId ?? throw new InvalidOperationException());
				
				User user = await _context
					.Users
					.FirstAsync(user => user.Id.Equals(UserId));
				user.Name = request.newName;

				_context.Users.Update(user);
				await _context.SaveChangesAsync(cancellationToken);
				
				return Unit.Value;
			}
		}
	}
}
