using Example.ExampleLeaders.Automation.Pages.Admin;
using Microsoft.Playwright.NUnit;

namespace Example.ExampleLeaders.Automation.Tests.Admin;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tasks : PageTest
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
    public async Task CreateTask()
    {
        //log in
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();

        //indicate log in as external user
        await loginPage.ClickLoginAsExternalButtonAsync();
        //login as user
        var dashboardPage = await loginPage.LoginAsync(_userName, _password);

        //click advanced search
        var searchPage = await dashboardPage.ClickAdvancedSearchAsync();

        //search active and select the first result
        var profilePage = await searchPage.FindAndSelectFirstActiveScholarAsync();

        //navigate to create task
        var tasksPage = await profilePage.ClickTasksTabAsync();
        var createTaskPage = await tasksPage.ClickCreateTaskAsync();

        //create task
        await createTaskPage.CreateStudentQuestionTask("TEST", "This is a test task", DateTime.Now.ToString("dd-MMM-yyyy"));
        tasksPage = await createTaskPage.ClickSaveAsync();

        //verify first task
        await Expect(tasksPage.FirstTask).ToHaveTextAsync("TEST [General]");
    }
}