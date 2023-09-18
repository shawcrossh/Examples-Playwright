using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ScholarPersonalInfoPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _lnkEdit;

    public const string UrlRef = "**/ScholarProfile/PersonalInfo/**";

    public ScholarPersonalInfoPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _lnkEdit = _page.GetByRole(AriaRole.Link, new() { Name = "»Edit" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<ScholarPersonalInfoEditPage> ClickEditAsync()
    {
        await _lnkEdit.ClickAsync();
        await _page.WaitForURLAsync(ScholarPersonalInfoEditPage.UrlRef);
        return new ScholarPersonalInfoEditPage(_page);
    }
}