using Example.ExampleLeaders.Automation.Pages.Admin;
using Microsoft.Playwright.NUnit;

namespace Example.ExampleLeaders.Automation.Tests.Admin;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class StudentProfile : PageTest
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
    public async Task EditPersonalInfo()
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
        
        //personal info 
        var infoPage = await profilePage.ClickPersonalInfoTabAsync();
        var editPage = await infoPage.ClickEditAsync();

        //update first name and verify
        infoPage = await editPage.UpdateFirstNameAndSave("TEST");

        var firstNameLabel = Page.GetByText("TEST", new() { Exact = true });
        await Expect(firstNameLabel).ToBeVisibleAsync();
    }
}