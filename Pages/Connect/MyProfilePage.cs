using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Connect;

public class MyProfilePage
{
    private readonly IPage _page;
    private readonly ILocator _tabBio;
    private readonly ILocator _txtBioQuestion1;
    private readonly ILocator _txtBioQuestion2;
    private readonly ILocator _btnUpdate;
    private readonly ILocator _txtMobilePhone;
    private readonly ILocator _txtHomePhone;
    private readonly ILocator _txtEmergencyPhone;

    private const string SuccessSelector = "div.green-message-box";

    public const string UrlRef = "**/MyProfile/ContactInformation";

    public MyProfilePage(IPage page)
    {
        _page = page;
        _tabBio = _page.GetByRole(AriaRole.Link, new() { NameString = "Bio" });
        _txtBioQuestion1 = _page.Locator("textarea").Nth(0);
        _txtBioQuestion2 = _page.Locator("textarea").Nth(1);
        _btnUpdate = _page.GetByRole(AriaRole.Button, new() { NameString = "Update Info" });
        _txtMobilePhone = _page.GetByLabel("Mobile Phone");
        _txtHomePhone = _page.Locator("#CurrentAddress_PhoneNumber");
        _txtEmergencyPhone = _page.GetByLabel("Phone Number *");
    }

    public async Task ClickBioTabAsync() => await _tabBio.ClickAsync();

    public async Task UpdateAllPhoneNumbers(string number)
    {
        await _txtMobilePhone.FillAsync(number);
        await _txtHomePhone.FillAsync(number);
        await _txtEmergencyPhone.FillAsync(number);
    }

    public async Task ClearBioQuestionsAsync()
    {
        await _txtBioQuestion1.ClearAsync();
        await _txtBioQuestion2.ClearAsync();
    }

    public async Task UpdateBioQuestion1(string text, bool replace = true)
    {
        if (replace)
        {
            await _txtBioQuestion1.ClearAsync();
        }

        await _txtBioQuestion1.FillAsync(text);
    }

    public async Task UpdateBioQuestion2(string text, bool replace = true)
    {
        if (replace)
        {
            await _txtBioQuestion2.ClearAsync();
        }

        await _txtBioQuestion2.FillAsync(text);
    }

    public async Task ClickUpdateAndWaitForResponseAsync()
    {
        await _btnUpdate.ClickAsync();
        await _page.WaitForSelectorAsync(SuccessSelector, new() { State = WaitForSelectorState.Visible });
    }
}