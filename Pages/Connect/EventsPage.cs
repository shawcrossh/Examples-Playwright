using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Connect;

public class EventsPage
{
    private readonly IPage _page;
    private readonly ILocator _tabAllEvents;
    private readonly ILocator _grdEvents;
    private readonly ILocator _lnkFirstEvent;

    public const string UrlRef = "**/Events";

    public EventsPage(IPage page)
    {
        _page = page;
        _tabAllEvents = _page.GetByRole(AriaRole.Tab).Nth(1);
        _grdEvents = _page.GetByRole(AriaRole.Tabpanel).GetByRole(AriaRole.Grid);
        _lnkFirstEvent = _grdEvents.GetByRole(AriaRole.Link).First;
    }

    public async Task ClickAllEventsTabAsync() => await _tabAllEvents.ClickAsync();

    public async Task<EventDetailsPage> ClickFirstEventAsync()
    {
        await _lnkFirstEvent.ClickAsync();
        await _page.WaitForURLAsync(EventDetailsPage.UrlRef);
        return new EventDetailsPage(_page);
    }
}