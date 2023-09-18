using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ScholarPersonalInfoEditPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _lnkEdit;
    private readonly ILocator _txtFirstName;
    private readonly ILocator _btnSave;

    public const string UrlRef = "**/ScholarProfile/EditPersonalInfo/**";

    public ScholarPersonalInfoEditPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _lnkEdit = _page.GetByRole(AriaRole.Link, new() { NameString = ">>Edit" });
        _txtFirstName = _page.GetByLabel("First Name"); 
        _btnSave = _page.GetByRole(AriaRole.Button, new() { NameString = "Save" }).First;
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<AddNotePage> ClickEditAsync()
    {
        await _lnkEdit.ClickAsync();
        await _page.WaitForURLAsync(AddNotePage.UrlRef);
        return new AddNotePage(_page);
    }

    public async Task<ScholarPersonalInfoPage> UpdateFirstNameAndSave(string newName)
    {
        await _txtFirstName.FillAsync(newName);
        await _btnSave.ClickAsync();
        await _page.WaitForURLAsync(ScholarPersonalInfoPage.UrlRef);
        return new ScholarPersonalInfoPage(_page);
    }


}