using MediatR;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Common;

namespace Template.Infrastructure.Common;

public static class MediatorExtensions
{
	public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
	{
		List<BaseEntity> entities = context.ChangeTracker
			.Entries<BaseEntity>()
			.Where(entry => entry.Entity.DomainEvents.Any())
			.Select(entry => entry.Entity)
			.ToList();

		List<BaseEvent> domainEvents = entities
			.SelectMany(entity => entity.DomainEvents)
			.ToList();

		entities.ToList().ForEach(e => e.ClearDomainEvents());

		foreach (BaseEvent domainEvent in domainEvents)
		{
			await mediator.Publish(domainEvent);
		}
	}
}
