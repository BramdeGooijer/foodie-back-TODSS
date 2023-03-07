using Template.Application.Common.Extensions;

namespace Template.Application.Logic.TodoLists.Commands;

public record UpdateTodoListCommand : IRequest
{
	public Guid Id { get; init; }

	public string? Title { get; init; }
}

public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
	private readonly IApplicationDbContext _context;

	public UpdateTodoListCommandValidator(IApplicationDbContext context)
	{
		_context = context;

		RuleFor(command => command.Title)
			.NotEmpty().WithMessage("Title is required.")
			.MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
			.MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
	}

	public async Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
	{
		return await _context.TodoLists
			.Where(todoList => !todoList.Id.Equals(model.Id))
			.AllAsync(todoList => todoList.Title != title, cancellationToken);
	}
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
{
	private readonly IApplicationDbContext _context;

	public UpdateTodoListCommandHandler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.TodoLists
			.FindOrNotFoundAsync(request.Id, cancellationToken);

		entity.Title = request.Title;

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
