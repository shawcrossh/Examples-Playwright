using Microsoft.Playwright.NUnit;
using Example.ExampleLeaders.Automation.Pages.Connect;

namespace Example.ExampleLeaders.Automation.Tests.Connect;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tasks : PageTest
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
    public async Task LogInAndNavigateTasks()
    {
        //log in
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();
        var dashboardPage = await loginPage.LoginAsync(_userName, _password);

        //go to connect tasks
        await dashboardPage.ClickContactUsAsync();
        var tasksPage = await dashboardPage.ClickTasksAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToConnectTasks.png" });

        //click the incomplete tasks tab
        await tasksPage.ClickIncompleteTabAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/IncompleteTasks.png" });

        //click all tasks tab
        await tasksPage.ClickAllTasksTabAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/AllTasks.png" });

        //click first task
        var taskDetailsPage = await tasksPage.ClickFirstTaskAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/OneTask.png" });

        //confirm one task
        await Expect(taskDetailsPage.TaskList).ToHaveCountAsync(1);
    }
}