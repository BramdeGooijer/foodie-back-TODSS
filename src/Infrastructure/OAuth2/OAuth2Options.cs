using System.ComponentModel.DataAnnotations;

namespace Template.Infrastructure.OAuth2;

public class OAuth2Options
{
	public const string OAuth2 = nameof(OAuth2);

	[Required] public required string Audience { get; set; }

	[Required] public required string Issuer { get; set; }

	[Required] public required string SecurityKey { get; set; }

	[Required] public required int TokenDuration { get; set; }
}
