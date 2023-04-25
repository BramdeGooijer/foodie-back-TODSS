namespace Template.Presentation.AcceptanceTests.Pages;

public class LoginPage : BasePage
{
	public LoginPage(IBrowser browser, IPage page)
	{
		Browser = browser;
		Page = page;
	}

	public override string PagePath => $"{BaseUrl}/authentication/login";

	public override IBrowser Browser { get; }

	public override IPage Page { get; set; }

	public Task SetEmail(string email)
	{
		return Page.FillAsync("#Input_Email", email);
	}

	public Task SetPassword(string password)
	{
		return Page.FillAsync("#Input_Password", password);
	}

	public Task ClickLogin()
	{
		return Page.Locator("#login-submit").ClickAsync();
	}

	public Task<string?> ProfileLinkText()
	{
		return Page.Locator("a[href='/authentication/profile']").TextContentAsync();
	}

	public Task<bool> InvalidLoginAttemptMessageVisible()
	{
		return Page.Locator("text=Invalid login attempt.").IsVisibleAsync();
	}
}
