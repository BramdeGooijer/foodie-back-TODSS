namespace Template.Application.Logic.TodoLists.Commands;

public record CreateTodoListCommand : IRequest<Guid>
{
	public string? Title { get; init; }
}

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
	private readonly IApplicationDbContext _context;

	public CreateTodoListCommandValidator(IApplicationDbContext context)
	{
		_context = context;

		RuleFor(command => command.Title)
			.NotEmpty().WithMessage("Title is required.")
			.MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
			.MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
	}

	public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
	{
		return await _context.TodoLists
			.AllAsync(todoList => todoList.Title != title, cancellationToken);
	}
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Guid>
{
	private readonly IApplicationDbContext _context;

	public CreateTodoListCommandHandler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
	{
		var entity = new TodoList
		{
			Title = request.Title
		};

		_context.TodoLists.Add(entity);

		await _context.SaveChangesAsync(cancellationToken);

		return entity.Id;
	}
}
