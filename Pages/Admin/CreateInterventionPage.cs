using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class CreateInterventionPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _lstClassification;
    private readonly ILocator _lstIssueType;
    private readonly ILocator _lstSupportNeeds;
    private readonly ILocator _btnSubmit;
    private readonly ILocator _lstInterventionType;

    public const string UrlRef = "**/ScholarInterventions/CreateIntervention?**";

    public CreateInterventionPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _lstClassification = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Classification *" });
        _lstIssueType = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Issue Type *" });
        _lstSupportNeeds = _page.Locator("#SelectedSupportNeeds_chosen");
        _btnSubmit = _page.GetByRole(AriaRole.Button, new() { NameString = "Submit" });
        _lstInterventionType = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Intervention Type *" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<InterventionsPage> ClickSubmitAsync()
    {
        await _btnSubmit.ClickAsync();
        await _page.WaitForURLAsync(InterventionsPage.UrlRef);
        return new InterventionsPage(_page);
    }

    public async Task SelectClassificationAsync(string classification)
    {
        await _lstClassification.SelectOptionAsync(new SelectOptionValue() { Label = classification });
    }

    public async Task SelectIssueTypeAsync(string type)
    {
        await _lstIssueType.SelectOptionAsync(new SelectOptionValue() { Label = type });
    }

    public async Task SelectSupportNeedsAsync(string option)
    {
        await _lstSupportNeeds.ClickAsync();
        await _lstSupportNeeds.GetByText(option).ClickAsync();
        await _lstInterventionType.IsVisibleAsync();
    }
}