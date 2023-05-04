namespace Template.Application.Logic.Oauth2.Models;

/// <summary>
///     OAuth 2.0 token response specified in <see href="https://tools.ietf.org/html/rfc6749#section-5.1">Section 5.1</see>.
/// </summary>
public class Token
{
	/// <summary>
	///     The access token issued by the authorization server.
	/// </summary>
	public string AccessToken { get; set; } = default!;

	/// <summary>
	///     The type of the <see cref="AccessToken">access token</see>
	///     issued as described in <see href="https://www.rfc-editor.org/rfc/rfc6749#section-7.1">Section 7.1</see>.
	///     Value is case insensitive.
	/// </summary>
	public TokenType TokenType { get; set; }

	/// <summary>
	///     The lifetime in seconds of the <see cref="AccessToken">access token</see>.
	/// </summary>
	public int ExpiresIn { get; set; }

	/// <summary>
	///     The refresh token, which can be used to obtain new <see cref="AccessToken">access token</see>
	///     using the '<see cref="GrantType.RefreshToken">RefreshToken</see>'
	///     authorization grant as described in <see href="https://www.rfc-editor.org/rfc/rfc6749#section-6">Section 6</see>.
	/// </summary>
	public string RefreshToken { get; set; } = default!;

	/// <summary>
	///     The scope of the <see cref="AccessToken">access token</see> as described by
	///     <see href="https://www.rfc-editor.org/rfc/rfc6749#section-3.3">Section 3.3</see>.
	/// </summary>
	public ICollection<string> Scope { get; set; } = Array.Empty<string>();

	/// <summary>
	///     The roles of the <see cref="AccessToken">access token</see>. For role based authentication.
	/// </summary>
	public ICollection<string> Roles { get; set; } = Array.Empty<string>();
}
