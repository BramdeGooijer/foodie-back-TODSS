namespace Template.Application.Common.Models;

public class PaginatedList<T>
{
	private const int DefaultPageSize = 20;
	private const int MaxPageSize = 100;

	public PaginatedList(IList<T> items, int count, int pageNumber, int pageSize)
	{
		PageNumber = pageNumber;
		PageSize = pageSize;
		TotalPages = pageSize > 0 ? (int)Math.Ceiling(count / (double)pageSize) : 0;
		TotalCount = count;
		Items = items;
	}

	public IList<T> Items { get; }

	public int PageNumber { get; }

	public int PageSize { get; }

	public int TotalPages { get; }

	public int TotalCount { get; }

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
		pageNumber = pageNumber is >= 0 ? pageNumber : 0;

		if (pageSize is not >= 0)
		{
			pageSize = DefaultPageSize;
		}

		if (pageSize > MaxPageSize)
		{
			pageSize = MaxPageSize;
		}

		List<TDestination> items = await source
			.Skip(pageNumber.Value * pageSize.Value)
			.Take(pageSize.Value)
			.MapToListAsync<TSource, TDestination>(configurationProvider, cancellationToken);

		int count = await source.CountAsync(cancellationToken);

		return new PaginatedList<TDestination>(items, count, pageNumber.Value, pageSize.Value);
	}
}
