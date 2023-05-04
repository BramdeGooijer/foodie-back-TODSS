namespace Template.Application.Common.Validators;

internal static class EntityValidators
{
	public static IRuleBuilderOptions<T, Guid> Exists<T, TEntity>(this IRuleBuilder<T, Guid> ruleBuilder, DbSet<TEntity> set)
		where TEntity : BaseEntity =>
		ruleBuilder
			.MustAsync(async (id, cancellationToken) =>
				!id.Equals(Guid.Empty) && await set.FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken) is not null)
			.WithErrorCode("NotFound")
			.WithMessage("{PropertyName} '{PropertyValue}' not found.");
}
