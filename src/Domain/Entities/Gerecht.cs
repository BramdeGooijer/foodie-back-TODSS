namespace Template.Domain.Entities;

public class Gerecht : BaseAuditableEntity
{
	public string naam {get; set;}
	public string subnaam {get; set;}
	public bool plusGerecht {get; set;}
	public string beschrijving {get; set;}
	public int bereidingsTijdMinuut { get; set;}
	public DateTime postDatum { get; set;}
	public DateTime editDatum { get; set;}


}
