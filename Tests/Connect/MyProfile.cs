using Example.ExampleLeaders.Automation.Pages.Connect;
using Microsoft.Playwright.NUnit;
using System.Text;

namespace Example.ExampleLeaders.Automation.Tests.Connect;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MyProfile : PageTest
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
    public async Task UpdateMyProfileBio()
    {
        const string newText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vel elit scelerisque mauris pellentesque. Tellus cras adipiscing enim eu turpis egestas. Venenatis urna cursus eget nunc scelerisque viverra mauris in aliquam. Volutpat lacus laoreet non curabitur gravida arcu ac tortor. Felis donec et odio pellentesque diam volutpat. Praesent semper feugiat nibh sed. Urna molestie at elementum eu facilisi";

        //log in and navigate
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();
        var dashboardPage = await loginPage.LoginAsync(_userName, _password);
        var profilePage = await dashboardPage.ClickMyProfileAsync();
        await profilePage.ClickBioTabAsync();

        //replace text in both bio questions
        await profilePage.UpdateBioQuestion1(newText);
        await profilePage.UpdateBioQuestion2(newText);
        await profilePage.ClickUpdateAndWaitForResponseAsync();
    }

    [Test]
    public async Task UpdateMyProfilePhones()
    {
        var newNumber = GenerateRandomPhoneNumber();

        //log in and navigate
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();
        var dashboardPage = await loginPage.LoginAsync(_userName, _password);
        var profilePage = await dashboardPage.ClickMyProfileAsync();

        //update all phone numbers
        await profilePage.UpdateAllPhoneNumbers(newNumber);
        await profilePage.ClickUpdateAndWaitForResponseAsync();
    }

    private string GenerateRandomPhoneNumber()
    {
        var randomPhone = new StringBuilder();

        //10 digits, starts with 0
        randomPhone.Append(0);

        for (int i = 0; i < 9; i++)
        {
            randomPhone.Append(new Random().Next(0, 9));
        }

        return randomPhone.ToString();
    }
}