using Example.ExampleLeaders.Automation.Pages.Admin;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Example.ExampleLeaders.Automation.Tests.Admin;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class StudentAcademics : PageTest
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
    public async Task EditAcademicsDegreePeriod()
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
        
        //academics info 
        var infoPage = await profilePage.ClickAcademicsTabAsync();
        var editPage = await infoPage.ClickEditDetailsAsync();

        //update degree period, save and verify
        await editPage.DegreePeriod.SelectOptionAsync(new SelectOptionValue() { Label = "3" });
        await editPage.ClickSaveAsync();
        await Expect(editPage.DegreePeriod).ToHaveValueAsync("3");
    }
}