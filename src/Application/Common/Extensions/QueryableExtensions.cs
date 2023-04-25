namespace Template.Application.Common.Extensions;

public static class QueryableExtensions
{
	/// <summary>
	///     Get first entity with <see cref="id" /> and if not found throws <see cref="NotFoundException" />.
	/// </summary>
	/// <param name="query"></param>
	/// <param name="id"></param>
	/// <param name="cancellationToken"></param>
	/// <exception cref="NotFoundException"></exception>
	public static async Task<TEntity> FindOrNotFoundAsync<TEntity>(this IQueryable<TEntity> query, Guid id,
		CancellationToken cancellationToken = default)
		where TEntity : BaseEntity
	{
		return await query.FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken)
		       ?? throw new NotFoundException(typeof(TEntity).Name, id.ToString());
	}
}
