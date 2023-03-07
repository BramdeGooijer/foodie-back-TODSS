namespace Template.Application.Common.Models;

public class PaginatedList<T>
{
	public IList<T> Items { get; }

	public int PageNumber { get; }

	public int TotalPages { get; }

	public int TotalCount { get; }

	public PaginatedList(IList<T> items, int count, int pageNumber, int pageSize)
	{
		PageNumber = pageNumber;
		TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		TotalCount = count;
		Items = items;
	}

	private const int DefaultPageNumber = 0;

	private const int DefaultPageSize = 20;

	public bool HasPreviousPage => PageNumber > 1;

	public bool HasNextPage => PageNumber < TotalPages;

	public static async Task<PaginatedList<TDestination>> CreateAsync<TSource, TDestination>(
		IQueryable<TSource> source,
		IConfigurationProvider configurationProvider,
		int? pageNumber,
		int? pageSize,
		CancellationToken cancellationToken = default)
		where TSource : BaseEntity
		where TDestination : IMapFrom<TSource>
	{
		pageNumber ??= DefaultPageNumber;
		pageSize ??= DefaultPageSize;

		var items = await source
			.Skip(pageNumber.Value * pageSize.Value)
			.Take(pageSize.Value)
			.MapToListAsync<TSource, TDestination>(configurationProvider, cancellationToken);

		var count = await source.CountAsync(cancellationToken);

		return new PaginatedList<TDestination>(items, count, pageNumber.Value, pageSize.Value);
	}
}
