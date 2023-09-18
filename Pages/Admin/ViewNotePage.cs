using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ViewNotePage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _lnkDelete;

    public const string UrlRef = "**/ScholarNotes/ViewNote/**";

    public ViewNotePage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _lnkDelete = _page.GetByRole(AriaRole.Link, new() { NameString = "Delete" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<DeleteNotePage> ClickDeleteAsync()
    {
        await _lnkDelete.ClickAsync();
        await _page.WaitForURLAsync(DeleteNotePage.UrlRef);
        return new DeleteNotePage(_page);
    }
}