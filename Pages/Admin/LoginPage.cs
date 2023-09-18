using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class LoginPage
{
    private readonly IPage _page;
    private readonly ILocator _txtEmail;
    private readonly ILocator _txtPassword;
    private readonly ILocator _lnkLogin;
    private readonly ILocator _btnLoginAsExternal;

    public LoginPage(IPage page)
    {
        _page = page;
        _btnLoginAsExternal = _page.GetByRole(AriaRole.Button, new() { NameString = "External User Account Login" });
        _txtEmail = _page.Locator("id=Email");
        _txtPassword = _page.Locator("id=Password");
        _lnkLogin = _page.Locator("text=Log In");
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync("https://admindemo.ExampleExampleLeaders.org/Account/Login");
    }

    public async Task ClickLoginAsExternalButtonAsync() => await _btnLoginAsExternal.ClickAsync();

    public async Task<DashboardPage> LoginAsync(string username, string password)
    {
        await _txtEmail.FillAsync(username);
        await _txtPassword.FillAsync(password);
        await _lnkLogin.ClickAsync();
        return new DashboardPage(_page);
    }
}