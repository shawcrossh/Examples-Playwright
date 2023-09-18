using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ScholarTaskCenterPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _btnCreateTask;
    private readonly ILocator _tblTasks;
    private readonly ILocator _lnkFirstTask;


    public const string UrlRef = "**/Tasks/Scholar/**";

    public ScholarTaskCenterPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _btnCreateTask = _page.GetByRole(AriaRole.Button, new() { NameString = "Create Task" });
        _tblTasks = _page.GetByRole(AriaRole.Grid);
        //body of table, first row, 3rd cell is the name+link
        _lnkFirstTask = _tblTasks.Locator("tbody tr:nth-child(1) td:nth-child(3)").GetByRole(AriaRole.Link).First;
    }

    public ILocator FirstTask => _lnkFirstTask;

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<ScholarTaskCreatePage> ClickCreateTaskAsync()
    {
        await _btnCreateTask.ClickAsync();
        await _page.WaitForURLAsync(ScholarTaskCreatePage.UrlRef);
        return new ScholarTaskCreatePage(_page);
    }
}