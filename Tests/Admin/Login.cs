using Example.ExampleLeaders.Automation.Pages.Admin;
using Microsoft.Playwright.NUnit;

namespace Example.ExampleLeaders.Automation.Tests.Admin;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Login : PageTest
{
    private string _userName = "";
    private string _password = "";

    [OneTimeSetUp]
    public void Setup()
    {
        _userName = Environment.GetEnvironmentVariable("ExampleAdminUser") ?? "";
        _password = Environment.GetEnvironmentVariable("ExampleAdminPassword") ?? "";
    }

    [Test]
    public async Task LogInAndLogOut()
    {
        //go to login page
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();

        //indicate log in as external user
        await loginPage.ClickLoginAsExternalButtonAsync();

        //login as user
        var dashboardPage = await loginPage.LoginAsync(_userName, _password);

        //log out of dashboard
        await dashboardPage.ClickLogoutAsync();
    }
}