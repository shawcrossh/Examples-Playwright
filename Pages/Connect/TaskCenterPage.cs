using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Connect;

public class TaskCenterPage
{
    private readonly IPage _page;
    private readonly ILocator _tabAllTasks;
    private readonly ILocator _tabIncomplete;
    private readonly ILocator _lnkFirstTask;
    private readonly ILocator _grdEvents;

    public const string UrlRef = "**/TaskCenter";

    public TaskCenterPage(IPage page)
    {
        _page = page;
        _tabAllTasks = _page.GetByRole(AriaRole.Tab, new() { NameString = "All Tasks" });
        _tabIncomplete = _page.GetByRole(AriaRole.Tab, new() { NameString = "Incomplete" });
        _grdEvents = _page.GetByRole(AriaRole.Tabpanel).GetByRole(AriaRole.Grid);
        _lnkFirstTask = _grdEvents.GetByRole(AriaRole.Link).First;
    }

    public async Task ClickAllTasksTabAsync() => await _tabAllTasks.ClickAsync();

    public async Task ClickIncompleteTabAsync() => await _tabIncomplete.ClickAsync();

    public async Task<TaskDetailsPage> ClickFirstTaskAsync()
    {
        await _lnkFirstTask.ClickAsync();
        await _page.WaitForURLAsync(TaskDetailsPage.UrlRef);
        return new TaskDetailsPage(_page);
    }
}