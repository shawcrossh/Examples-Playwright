using Microsoft.Playwright.NUnit;
using Example.ExampleLeaders.Automation.Pages.Connect;

namespace Example.ExampleLeaders.Automation.Tests.Connect;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Dashboard : PageTest
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
    public async Task LogInAndNavigateDashboard()
    {
        //go to login page
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();

        //login as user
        var dashboardPage = await loginPage.LoginAsync(_userName, _password);

        //click dashboard's menu options
        await dashboardPage.ClickMyProfileAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToMyProfile.png" });

        await dashboardPage.ClickDocumentsAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToDocuments.png" });

        await dashboardPage.ClickTasksAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToTasks.png" });

        await dashboardPage.ClickEventsAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToEvents.png" });

        //await dashboardPage.ClickTutoringAsync();
        //await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToTutoring.png" });

        await dashboardPage.ClickDirectoryAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToDirectory.png" });

        await dashboardPage.ClickResourcesAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToResources.png" });

        await dashboardPage.ClickContactUsAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToContactUs.png" });

        //log out
        await dashboardPage.ClickLogoutAsync();
    }
}