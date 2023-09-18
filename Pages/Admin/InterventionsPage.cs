using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class InterventionsPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _lnkCreateIntervention;

    public const string UrlRef = "**/ScholarInterventions/Index/**";

    public InterventionsPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _lnkCreateIntervention = _page.GetByRole(AriaRole.Button, new() { NameString = "Create Intervention" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<CreateInterventionPage> ClickCreateInterventionAsync()
    {
        await _lnkCreateIntervention.ClickAsync();
        await _page.WaitForURLAsync(CreateInterventionPage.UrlRef);
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return new CreateInterventionPage(_page);
    }

    public async Task<EditInterventionPage> ClickEditInterventionAsync(string type)
    {
        var editLink = _page.GetByRole(AriaRole.Row, new() { NameString = type }).GetByRole(AriaRole.Link);
        await editLink.ClickAsync();
        await _page.WaitForURLAsync(EditInterventionPage.UrlRef);
        return new EditInterventionPage(_page);
    }
}