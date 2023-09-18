using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Connect;

public class DashboardPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _lnkHome;
    private readonly ILocator _lnkMyProfile;
    private readonly ILocator _lnkDocuments;
    private readonly ILocator _lnkTasks;
    private readonly ILocator _lnkEvents;
    private readonly ILocator _lnkDirectory;
    private readonly ILocator _lnkResources;
    private readonly ILocator _lnkContactUs;
    private readonly ILocator _lnkViewAllEvents;

    public const string UrlRef = "**/Home";

    public DashboardPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.Locator("#top_navigation >> a", new() { HasTextString = "Log Out" });
        _lnkHome = _page.Locator("text=Home");
        _lnkMyProfile = _page.GetByRole(AriaRole.Link, new() { NameString = "My Profile" });
        _lnkDocuments = _page.GetByRole(AriaRole.Link, new() { NameString = "Documents" });
        _lnkTasks = _page.GetByRole(AriaRole.Link, new() { NameString = "Tasks" });
        _lnkEvents = _page.GetByRole(AriaRole.Link, new() { NameString = "Events" });
        _lnkDirectory = _page.GetByRole(AriaRole.Link, new() { NameString = "Directory" });
        _lnkResources = _page.GetByRole(AriaRole.Link, new() { NameString = "Resources" });
        _lnkContactUs = _page.GetByRole(AriaRole.Link, new() { NameString = "Contact Us" });
        _lnkViewAllEvents = _page.GetByRole(AriaRole.Link, new() { NameString = "View all Events" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task ClickEventsAsync() => await _lnkEvents.ClickAsync();

    public async Task ClickDirectoryAsync() => await _lnkDirectory.ClickAsync();

    public async Task ClickResourcesAsync() => await _lnkResources.ClickAsync();

    public async Task ClickContactUsAsync() => await _lnkContactUs.ClickAsync();

    public async Task<DashboardPage> ClickHomeAsync()
    {
        await _lnkHome.ClickAsync();
        await _page.WaitForURLAsync(UrlRef);
        return new DashboardPage(_page);
    }

    public async Task<MyProfilePage> ClickMyProfileAsync()
    {
        await _lnkMyProfile.ClickAsync();
        await _page.WaitForURLAsync(MyProfilePage.UrlRef);
        return new MyProfilePage(_page);
    }

    public async Task<EventsPage> ClickViewAllEventsAsync()
    {
        await _lnkViewAllEvents.ClickAsync();
        await _page.WaitForURLAsync(EventsPage.UrlRef + "#all_events");
        return new EventsPage(_page);
    }

    public async Task<DocumentCenterPage> ClickDocumentsAsync()
    {
        await _lnkDocuments.ClickAsync();
        await _page.WaitForURLAsync(DocumentCenterPage.UrlRef);
        return new DocumentCenterPage(_page);
    }

    public async Task<TaskCenterPage> ClickTasksAsync()
    {
        await _lnkTasks.ClickAsync();
        await _page.WaitForURLAsync(TaskCenterPage.UrlRef);
        return new TaskCenterPage(_page);
    }
}