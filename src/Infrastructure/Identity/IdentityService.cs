using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template.Application.Common.Interfaces;
using Template.Application.Common.Models;

namespace Template.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
	private readonly IAuthorizationService _authorizationService;
	private readonly SignInManager<IdentityUser> _signInManager;
	private readonly IUserClaimsPrincipalFactory<IdentityUser> _userClaimsPrincipalFactory;
	private readonly UserManager<IdentityUser> _userManager;

	public IdentityService(
		UserManager<IdentityUser> userManager,
		SignInManager<IdentityUser> signInManager,
		IUserClaimsPrincipalFactory<IdentityUser> userClaimsPrincipalFactory,
		IAuthorizationService authorizationService)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_userClaimsPrincipalFactory = userClaimsPrincipalFactory;
		_authorizationService = authorizationService;
	}

	public async Task<string?> GetUserIdAsync(string userName)
	{
		IdentityUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());

		return user?.Id;
	}

	public async Task<string?> GetUserNameAsync(string userId)
	{
		IdentityUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

		return user?.Email;
	}

	public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
	{
		IdentityUser user = new()
		{
			UserName = userName,
			Email = userName,
			EmailConfirmed = true
		};

		IdentityResult result = await _userManager.CreateAsync(user, password);

		return (result.ToApplicationResult(), user.Id);
	}

	public async Task<bool> CheckPasswordSignInAsync(string userName, string password)
	{
		IdentityUser? user = await _userManager.FindByNameAsync(userName);

		if (user is null)
		{
			return false;
		}

		SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

		return result.Succeeded;
	}

	public async Task<IList<string>> GetRolesAsync(string userName)
	{
		IdentityUser? user = await _userManager.FindByNameAsync(userName);

		return user is not null
			? await _userManager.GetRolesAsync(user)
			: new List<string>();
	}

	public async Task<bool> IsInRoleAsync(string userId, string role)
	{
		IdentityUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

		return user is not null && await _userManager.IsInRoleAsync(user, role);
	}

	public async Task<bool> AuthorizeAsync(string userId, string policyName)
	{
		IdentityUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

		if (user == null)
		{
			return false;
		}

		ClaimsPrincipal principal = await _userClaimsPrincipalFactory.CreateAsync(user);

		AuthorizationResult result = await _authorizationService.AuthorizeAsync(principal, policyName);

		return result.Succeeded;
	}

	public async Task<Result> DeleteUserAsync(string userId)
	{
		IdentityUser? user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

		return user is not null
			? await DeleteUserAsync(user)
			: Result.Success();
	}

	internal async Task<IdentityUser?> FindByNameAsync(string userName) => await _userManager.FindByNameAsync(userName);

	internal async Task<Result> DeleteUserAsync(IdentityUser user)
	{
		IdentityResult result = await _userManager.DeleteAsync(user);

		return result.ToApplicationResult();
	}
}
