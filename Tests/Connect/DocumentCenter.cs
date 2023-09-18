using Example.ExampleLeaders.Automation.Pages.Connect;
using Microsoft.Playwright.NUnit;

namespace Example.ExampleLeaders.Automation.Tests.Connect;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class DocumentCenter : PageTest
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
    public async Task LogInAndUploadDocument()
    {
        //log in
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();
        var dashboardPage = await loginPage.LoginAsync(_userName, _password);

        //go to connect documents
        var documentsPage = await dashboardPage.ClickDocumentsAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DashboardToDocuments.png" });

        //ready document upload
        await documentsPage.SetUploadFileAsync("Application", "TestFile1.docx");
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DocumentReadyToUpload.png" });

        //upload
        await documentsPage.ClickUploadAndWaitForResponseAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/DocumentUploaded.png" });
    }
}