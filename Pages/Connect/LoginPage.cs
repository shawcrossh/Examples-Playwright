using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Connect;

public class LoginPage
{
    private readonly IPage _page;
    private readonly ILocator _txtEmail;
    private readonly ILocator _txtPassword;
    private readonly ILocator _lnkLogin;

    public LoginPage(IPage page)
    {
        _page = page;
        _txtEmail = _page.Locator("id=Email");
        _txtPassword = _page.Locator("id=Password");
        _lnkLogin = _page.Locator("text=Login");
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync("https://connectdemo.ExampleExampleLeaders.org/Account/Login");
    }

    public async Task<DashboardPage> LoginAsync(string username, string password)
    {
        await _txtEmail.FillAsync(username);
        await _txtPassword.FillAsync(password);
        await _lnkLogin.ClickAsync();
        await _page.WaitForURLAsync(DashboardPage.UrlRef);
        return new DashboardPage(_page);
    }
}