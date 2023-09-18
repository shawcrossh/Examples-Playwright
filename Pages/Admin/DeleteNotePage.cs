using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class DeleteNotePage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _btnDelete;

    public const string UrlRef = "**/ScholarNotes/DeleteNote/**";

    public DeleteNotePage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _btnDelete = _page.GetByRole(AriaRole.Button, new() { NameString = "Delete" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<ScholarProfilePage> ClickDeleteAsync()
    {
        await _btnDelete.ClickAsync();
        await _page.WaitForURLAsync(ScholarProfilePage.UrlRef);
        return new ScholarProfilePage(_page);
    }
}