using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Connect;

public class EventDetailsPage
{
    private readonly IPage _page;

    public const string UrlRef = "**/Events/EventDetails/**";

    public EventDetailsPage(IPage page)
    {
        _page = page;
        EventList = _page.Locator("#events-list");
    }

    public ILocator EventList { get; }
}