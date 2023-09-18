using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ScholarAcademicsPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _lnkEditDetails;

    public const string UrlRef = "**/ScholarAcademic/Index/**";

    public ScholarAcademicsPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _lnkEditDetails = _page.GetByRole(AriaRole.Link, new() { Name = "»Edit Details" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<ScholarAcademicsEditPage> ClickEditDetailsAsync()
    {
        await _lnkEditDetails.ClickAsync();
        await _page.WaitForURLAsync(ScholarAcademicsEditPage.UrlRef);
        return new ScholarAcademicsEditPage(_page);
    }
}