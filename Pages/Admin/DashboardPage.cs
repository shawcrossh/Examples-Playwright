using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class DashboardPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _lnkAdvancedSearch;
    private readonly ILocator _lnkDocuments;
    private readonly ILocator _lnkMore;

    public DashboardPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _lnkAdvancedSearch = _page.GetByRole(AriaRole.Link, new() { NameString = "Advanced" });
        _lnkMore = _page.GetByRole(AriaRole.Link, new() { Name = "More ▼" });
        _lnkDocuments = _page.GetByRole(AriaRole.Link, new() { Name = "Documents" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<AdvancedSearchPage> ClickAdvancedSearchAsync()
    {
        await _lnkAdvancedSearch.ClickAsync();
        await _page.WaitForURLAsync(AdvancedSearchPage.UrlRef);
        return new AdvancedSearchPage(_page);
    }

    public async Task<ScholarDocumentsPage> ClickDocumentsAsync()
    {
        await _lnkMore.ClickAsync();
        await _lnkDocuments.ClickAsync();
        await _page.WaitForURLAsync(ScholarDocumentsPage.UrlRef);
        return new ScholarDocumentsPage(_page);
    }
}