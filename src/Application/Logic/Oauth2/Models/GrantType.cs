namespace Template.Application.Logic.Oauth2.Models;

/// <summary>
///     OAuth 2.0 grant types specified by <see href="https://tools.ietf.org/html/rfc6749">RFC6749</see>
/// </summary>
public enum GrantType
{
	Password,
	AuthorizationCode,
	Implicit,
	ClientCredentials,
	RefreshToken
}
