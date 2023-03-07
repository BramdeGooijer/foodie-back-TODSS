namespace Template.Application.Common.Mappings;

public static class MappingExtensions
{
	public static TDestination Map<TSource, TDestination>(
		this TSource source,
		IConfigurationProvider configurationProvider)
		where TSource : BaseEntity
		where TDestination : IMapFrom<TSource> =>
		configurationProvider.CreateMapper()
			.Map<TDestination>(source);

	public static async Task<TDestination> MapAsync<TSource, TDestination>(
		this Task<TSource> source,
		IConfigurationProvider configurationProvider)
		where TSource : BaseEntity
		where TDestination : IMapFrom<TSource> =>
		configurationProvider.CreateMapper()
			.Map<TDestination>(await source);

	public static async Task<List<TDestination>> MapToListAsync<TSource, TDestination>(
		this IQueryable<TSource> queryable,
		IConfigurationProvider configurationProvider,
		CancellationToken cancellationToken = default)
		where TSource : BaseEntity
		where TDestination : IMapFrom<TSource> =>
		configurationProvider
			.CreateMapper()
			.Map<List<TDestination>>(await queryable.AsNoTracking().ToListAsync(cancellationToken));

	public static async Task<PaginatedList<TDestination>> MapToPaginatedListAsync<TSource, TDestination>(
		this IQueryable<TSource> queryable,
		IConfigurationProvider configurationProvider,
		int? pageNumber,
		int? pageSize,
		CancellationToken cancellationToken = default)
		where TSource : BaseEntity
		where TDestination : IMapFrom<TSource> =>
		await PaginatedList<TDestination>
			.CreateAsync<TSource, TDestination>(queryable.AsNoTracking(), configurationProvider, pageNumber, pageSize, cancellationToken);
}
