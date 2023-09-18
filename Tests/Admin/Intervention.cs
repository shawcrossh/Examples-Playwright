using Example.ExampleLeaders.Automation.Pages.Admin;
using Microsoft.Playwright.NUnit;

namespace Example.ExampleLeaders.Automation.Tests.Admin;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Intervention : PageTest
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
    public async Task CreateAndEditInterviewPrepIntervention()
    {
        //go to login page
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

        //navigate to create
        var interventionsPage = await profilePage.ClickInterventionsTabAsync();

        var createPage = await interventionsPage.ClickCreateInterventionAsync();

        //set intervention options
        await createPage.SelectClassificationAsync("Employment");
        await createPage.SelectIssueTypeAsync("Career Readiness");
        await createPage.SelectSupportNeedsAsync("Interview preparation");

        //submit intervention
        await createPage.ClickSubmitAsync();

        ////edit and add milestone
        //var editPage = await interventionsPage.ClickEditInterventionAsync("Interview preparation");

        //await editPage.ClickAddMilestoneAsync();

        //await editPage.AddMilestoneInfoAsync("Test", "Ad hoc consultation", DateTime.Now.AddDays(3).ToString("dd-MMM-yyyy"));

        ////submit with milestone
        //interventionsPage = await editPage.ClickSubmitAsync();

        ////end intervention
        //editPage = await interventionsPage.ClickEditInterventionAsync("Interview preparation");
        //await editPage.EndInterventionAsync(DateTime.Now.ToString("dd-MMM-yyyy"), "Medium", "Test");

        ////submit edits
        //await editPage.ClickSubmitAsync();
    }

    [Test]
    public async Task CreateAndEditPersistenceIntervention()
    {
        //go to login page
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

        //navigate to create
        var interventionsPage = await profilePage.ClickInterventionsTabAsync();

        var createPage = await interventionsPage.ClickCreateInterventionAsync();

        //set intervention options
        await createPage.SelectClassificationAsync("Persistence");
        await createPage.SelectIssueTypeAsync("Laptop");
        await createPage.SelectSupportNeedsAsync("Hardware support");

        //submit intervention
        interventionsPage = await createPage.ClickSubmitAsync();

        //end intervention
        var editPage = await interventionsPage.ClickEditInterventionAsync("Laptop");

        await editPage.EndInterventionAsync(DateTime.Now.ToString("dd-MMM-yyyy"), "Medium", "Test");

        //submit edits
        await editPage.ClickSubmitAsync();
    }
}