using Microsoft.Playwright.NUnit;
using Example.ExampleLeaders.Automation.Pages.Connect;

namespace Example.ExampleLeaders.Automation.Tests.Connect;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Events : PageTest
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
    public async Task ViewAnEvent()
    {
        //log in
        var loginPage = new LoginPage(Page);
        await loginPage.GotoAsync();
        var dashboardPage = await loginPage.LoginAsync(_userName, _password);

        //click "View all Events" (link on page vs. Events tab)
        var eventsPage = await dashboardPage.ClickViewAllEventsAsync();

        //view an event
        await eventsPage.ClickAllEventsTabAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/AllEvents.png" });

        var eventDetailsPage = await eventsPage.ClickFirstEventAsync();
        await Page.ScreenshotAsync(new() { Path = "./screenshots/OneEvent.png" });

        //confirm one event
        await Expect(eventDetailsPage.EventList).ToHaveCountAsync(1);
    }
}