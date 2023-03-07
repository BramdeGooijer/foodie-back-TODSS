using Microsoft.Extensions.Configuration;
using Template.Application.Logic.Oauth2.Models;

namespace Template.Application.Logic.Oauth2.Commands;

[AllowAnonymous]
public record CreateTokenCommand : IRequest<Token>
{
	public GrantType? GrantType { get; init; }

	public string? Username { get; init; }

	public string? Password { get; init; }

	public string? RefreshToken { get; init; }

	public string? RedirectUri { get; init; }
	
	public Guid? ClientId { get; init; }

	public string? ClientSecret { get; init; }

	public string? Code { get; init; }

	public IList<ScopeType>? Scope { get; init; }
}

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
	public CreateTokenCommandValidator(IIdentityService identityService, ITokenService tokenService)
	{
		RuleFor(command => command.GrantType)
			.NotEmpty()
			.Must(grantType => grantType is GrantType.Password or GrantType.RefreshToken)
			.WithMessage("UnsupportedGrantType");

		RuleFor(command => command.RedirectUri)
			.Url();

		RuleFor(command => command.Username)
			.NotEmpty()
			.EmailAddress();

		When(command => command.GrantType is GrantType.Password && command.Username is not null, () =>
			RuleFor(command => command.Password)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.MustAsync((command, _, _) => identityService.CheckPasswordSignInAsync(command.Username!, command.Password!))
				.WithMessage("UsernameOrPasswordIncorrect"));
		
		When(command => command.GrantType is GrantType.RefreshToken && command.Username is not null, () =>
			RuleFor(command => command.RefreshToken)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.Length(44)
				.WithMessage("InvalidFormat")
				.MustAsync((command, _, _) => tokenService.CheckRefreshTokenAsync(command.Username!, command.RefreshToken!, command.ClientId!.Value))
				.WithMessage("UsernameOrRefreshTokenIncorrect"));
	}
}

internal class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, Token>
{
	private readonly ITokenService _tokenService;
	private readonly IConfiguration _configuration;
	private readonly IIdentityService _identityService;

	public CreateTokenCommandHandler(ITokenService tokenService, IIdentityService identityService, IConfiguration configuration)
	{
		_tokenService = tokenService;
		_identityService = identityService;
		_configuration = configuration;
	}

	public async Task<Token> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
	{
		return new Token
		{
			AccessToken = await _tokenService.CreateAccessTokenAsync(request.Username!),
			TokenType = TokenType.Bearer,
			ExpiresIn = int.Parse(_configuration["Authentication:Oauth2:TokenDuration"]!),
			RefreshToken = await _tokenService.CreateRefreshTokenAsync(request.Username!, request.ClientId!.Value),
			Roles = await _identityService.GetRolesAsync(request.Username!) 
		};
	}
}
