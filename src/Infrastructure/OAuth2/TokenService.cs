using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Template.Application.Common.Exceptions;
using Template.Application.Common.Interfaces;
using Template.Infrastructure.Identity;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.OAuth2;

public class TokenService : ITokenService
{
	private readonly ApplicationDbContext _context;
	private readonly IDateTime _dateTime;
	private readonly IdentityService _identityService;
	private readonly OAuth2Options _oAuth2Options;

	public TokenService(IdentityService identityService, ApplicationDbContext context, IDateTime dateTime, IOptions<OAuth2Options> oauth2Options)
	{
		_identityService = identityService;
		_context = context;
		_dateTime = dateTime;
		_oAuth2Options = oauth2Options.Value;
	}

	public async Task<string> CreateAccessTokenAsync(string userName)
	{
		IdentityUser user = await _identityService.FindByNameAsync(userName)
		                    ?? throw new UnauthorizedAccessException();

		var tokenHandler = new JwtSecurityTokenHandler();
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
				new(ClaimTypes.NameIdentifier, user.Id)
			}),
			Expires = _dateTime.Now.AddMilliseconds(_oAuth2Options.TokenDuration),
			IssuedAt = _dateTime.Now,
			Issuer = _oAuth2Options.Issuer,
			Audience = _oAuth2Options.Audience,
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_oAuth2Options.SecurityKey)),
				SecurityAlgorithms.HmacSha256Signature)
		};

		foreach (var role in await _identityService.GetRolesAsync(userName))
		{
			tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
		}

		return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
	}

	public async Task<string> CreateRefreshTokenAsync(string userName, Guid clientId)
	{
		IdentityUser user = await _identityService.FindByNameAsync(userName)
		                    ?? throw new UnauthorizedAccessException();

		Client client = _context.Clients.FirstOrDefault(client => client.Id == clientId) ??
		                throw new NotFoundException(nameof(Client), clientId);

		var refreshToken = new RefreshToken
		{
			Identity = user,
			Client = client
		};

		_context.RefreshTokens.RemoveRange(await _context.RefreshTokens.Where(rt => rt.Identity.Id == user.Id && rt.Client.Id == clientId)
			.ToListAsync());

		await _context.RefreshTokens.AddAsync(refreshToken);

		await _context.SaveChangesAsync();

		return refreshToken.Token;
	}

	public async Task<bool> CheckRefreshTokenAsync(string userName, string token, Guid clientId)
	{
		RefreshToken? refreshToken = await _context.RefreshTokens
			.Include(rt => rt.Identity)
			.Include(rt => rt.Client)
			.FirstOrDefaultAsync(rt => rt.Token == token);

		if (refreshToken?.Identity.NormalizedUserName is null)
		{
			return false;
		}

		if (refreshToken.Identity.NormalizedEmail != userName.ToUpper())
		{
			return false;
		}

		if (refreshToken.Client.Id != clientId)
		{
			return false;
		}

		_context.RefreshTokens.Remove(refreshToken);

		await _context.SaveChangesAsync();

		return true;
	}

	public async Task RemoveRefreshTokensAsync(string userName, Guid clientId)
	{
		IdentityUser user = await _identityService.FindByNameAsync(userName) ??
		                    throw new ForbiddenAccessException();

		_context.RefreshTokens.RemoveRange(await _context.RefreshTokens.Where(rt => rt.Identity.Id == user.Id && rt.Client.Id == clientId)
			.ToListAsync());

		await _context.SaveChangesAsync();
	}

	internal string CreateInternalNetworkAccessToken()
	{
		DateTime now = _dateTime.Now;
		var tokenHandler = new JwtSecurityTokenHandler();
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
				new(ClaimTypes.Role, "InternalNetwork")
			}),
			Expires = now.AddMinutes(1),
			IssuedAt = now,
			Issuer = _oAuth2Options.Issuer,
			Audience = _oAuth2Options.Audience,
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_oAuth2Options.SecurityKey)),
				SecurityAlgorithms.HmacSha256Signature)
		};

		return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
	}
}
