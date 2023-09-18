using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ScholarAcademicsEditPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _btnSave;
    private readonly ILocator _lstDegreePeriod;

    public const string UrlRef = "**/ScholarAcademic/EditAcademicDetails/**";

    public ScholarAcademicsEditPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _btnSave = _page.GetByRole(AriaRole.Button, new() { NameString = "Save" }).First;
        _lstDegreePeriod = _page.Locator("#AcademicDetails_0__AcademicTermDetails_2__DegreePeriod");
    }

    public ILocator DegreePeriod => _lstDegreePeriod;

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task ClickSaveAsync() => await _btnSave.ClickAsync();
}