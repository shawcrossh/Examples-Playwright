using Example.ExampleLeaders.Automation.Pages.Admin;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;

namespace Example.ExampleLeaders.Automation.Tests.Admin;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Search : PageTest
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
    public async Task SearchActiveWithUniversity()
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

        //preset university 
        await searchPage.FilterOnUniversity("SUN");
        await searchPage.SearchActive();

        //verify results
        await Expect(searchPage.FirstStatus).ToHaveTextAsync("Active");
        await Expect(searchPage.FirstUniversity).ToHaveTextAsync("SUN");
    }

    [Test]
    public async Task SearchActiveByName()
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

        //preset university 
        await searchPage.SearchActiveByName("a");

        //verify results
        await Expect(searchPage.FirstPerson).ToContainTextAsync(new Regex("^\\A"));
    }
}