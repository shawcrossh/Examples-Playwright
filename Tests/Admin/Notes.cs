using Example.ExampleLeaders.Automation.Pages.Admin;
using Microsoft.Playwright.NUnit;

namespace Example.ExampleLeaders.Automation.Tests.Admin;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Notes : PageTest
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
    public async Task AddAndDeleteNote()
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

        //navigate to notes
        var notesPage = await profilePage.ClickNotesButtonAsync();

        //configure note details
        await notesPage.ClickContactNoAsync();
        await notesPage.SetCategoryAsync("Academic");
        await notesPage.SetSummaryAsync("test summary");
        await notesPage.SetTimeSpentAsync("5 Minutes");
        await notesPage.SetNoteAsync("test note");

        //save
        profilePage = await notesPage.ClickSaveAsync();

        //from summary, click note we just added
        var viewPage = await profilePage.ClickTestNoteAsync();

        //click note to edit/delete
        var deletePage = await viewPage.ClickDeleteAsync();

        //delete note
        profilePage = await deletePage.ClickDeleteAsync();

        //verify note is no longer present
        Assert.That(await profilePage.IsTestNoteVisible(), Is.False);
    }
}