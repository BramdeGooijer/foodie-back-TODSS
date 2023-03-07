namespace Template.Application.Logic.TodoItems.Commands;

public record UpdateTodoItemDetailCommand : IRequest
{
	public Guid Id { get; init; }

	public Guid ListId { get; init; }

	public PriorityLevel Priority { get; init; }

	public string? Note { get; init; }
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
	private readonly IApplicationDbContext _context;

	public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.TodoItems
			.FindAsync(new object[] { request.Id }, cancellationToken);

		if (entity is null)
			throw new NotFoundException(nameof(TodoItem), request.Id);

		entity.ListId = request.ListId;
		entity.Priority = request.Priority;
		entity.Note = request.Note;

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
