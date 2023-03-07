namespace Template.Domain.Common;

public abstract class BaseSoftDeletableEntity : BaseEntity
{
	public DateTime? DeletedAt { get; set; }
}
