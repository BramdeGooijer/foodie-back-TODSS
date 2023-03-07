using Template.Application.Common.Extensions;

namespace Template.Application.Logic.TodoItems.Commands;

public record UpdateTodoItemCommand : IRequest
{
	public Guid Id { get; init; }

	public string? Title { get; init; }

	public bool Done { get; init; }
}

public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
{
	public UpdateTodoItemCommandValidator()
	{
		RuleFor(command => command.Title)
			.NotEmpty()
			.MaximumLength(200);
	}
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
	private readonly IApplicationDbContext _context;

	public UpdateTodoItemCommandHandler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.TodoItems
			.FindOrNotFoundAsync(request.Id, cancellationToken);

		entity.Title = request.Title;
		entity.Done = request.Done;

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
