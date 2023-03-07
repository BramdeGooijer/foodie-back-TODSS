namespace Template.Application.Logic.TodoItems.Commands;

public record CreateTodoItemCommand : IRequest<Guid>
{
	public Guid ListId { get; init; }

	public string? Title { get; init; }
}

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
	public CreateTodoItemCommandValidator()
	{
		RuleFor(command => command.Title)
			.MaximumLength(200)
			.NotEmpty();
	}
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
{
	private readonly IApplicationDbContext _context;

	public CreateTodoItemCommandHandler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
	{
		var entity = new TodoItem
		{
			ListId = request.ListId,
			Title = request.Title,
			Done = false
		};

		entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

		_context.TodoItems.Add(entity);

		await _context.SaveChangesAsync(cancellationToken);

		return entity.Id;
	}
}
