using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Connect;

public class TaskDetailsPage
{
    private readonly IPage _page;

    public const string UrlRef = "**/TaskCenter/TaskDetails/**";

    public TaskDetailsPage(IPage page)
    {
        _page = page;
        TaskList = _page.Locator("#tasks-list");
    }

    public ILocator TaskList { get; }
}