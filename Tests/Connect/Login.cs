using Microsoft.Playwright.NUnit;
using Example.ExampleLeaders.Automation.Pages.Connect;

namespace Example.ExampleLeaders.Automation.Tests.Connect;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Login : PageTest
{
    private string _userName = "";
    private string _password = "";

    [OneTimeSetUp]
    public void Setup()
    {
        _userName = Environment.GetEnvironmentVariable("ConnectUser") ?? "";
        _password = Environment.GetEnvironmentVariable("ConnectPassword") ?? "";
    }

    [Test]
    public async Task LogInAndLogOut()
    {
        //go to login page
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/Login.png" });

        //login as user
        var dashboardPage = await loginPage.LoginAsync(_userName, _password);
        await Page.ScreenshotAsync(new() { Path = "./screenshots/LoginToDashboard.png" });

        //log out of dashboard
        await dashboardPage.ClickLogoutAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/LogOutFromDashboard.png" });
    }
}