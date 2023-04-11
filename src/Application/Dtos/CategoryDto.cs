namespace Template.Application.Dtos;

public record CategoryDto : IMapFrom<Category>
{
	public required Guid Id { get; set; }
	public required string Name { get; init; }
}
