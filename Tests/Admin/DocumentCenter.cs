using Example.ExampleLeaders.Automation.Pages.Admin;
using Microsoft.Playwright.NUnit;

namespace Example.ExampleLeaders.Automation.Tests.Admin;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class DocumentCenter : PageTest
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
    public async Task LogInAndUploadDocument()
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

        //go to documents
        var documentsPage = await dashboardPage.ClickDocumentsAsync();
        var documentUploadPage = await documentsPage.ClickUploadAsync();

        //ready document upload
        await documentUploadPage.SetUploadFileAsync("CV", "TestFile1.docx");

        //upload and verify
        documentsPage = await documentUploadPage.ClickUploadAsync();
        await Expect(documentsPage.FirstRow).ToContainTextAsync("CV");
    }
}