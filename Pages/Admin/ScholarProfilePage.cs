using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ScholarProfilePage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _lnkInterventions;
    private readonly ILocator _btnAddNote;
    private readonly ILocator _lnkTestNote;
    private readonly ILocator _lnkPersonalInfo;
    private readonly ILocator _lnkTasks;
    private readonly ILocator _lnkAcademics;

    public const string UrlRef = "**/ScholarProfile/Summary/**";

    public ScholarProfilePage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _lnkInterventions = _page.GetByRole(AriaRole.Link, new() { NameString = "Interventions" });
        _lnkPersonalInfo = _page.GetByRole(AriaRole.Link, new() { NameString = "Personal Info" });
        _btnAddNote = _page.GetByRole(AriaRole.Button, new() { NameString = "Add Note" });
        _lnkTestNote = _page.GetByRole(AriaRole.Link, new() { Name = "test" });
        _lnkTasks = _page.GetByRole(AriaRole.Link, new() { NameString = "Tasks" });
        _lnkAcademics = _page.GetByRole(AriaRole.Link, new() { NameString = "Academics" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<AddNotePage> ClickNotesButtonAsync()
    {
        await _btnAddNote.ClickAsync();
        await _page.WaitForURLAsync(AddNotePage.UrlRef);
        return new AddNotePage(_page);
    }

    public async Task<InterventionsPage> ClickInterventionsTabAsync()
    {
        await _lnkInterventions.ClickAsync();
        await _page.WaitForURLAsync(InterventionsPage.UrlRef);
        return new InterventionsPage(_page);
    }

    public async Task<ViewNotePage> ClickTestNoteAsync()
    {
        await _lnkTestNote.ClickAsync();
        await _page.WaitForURLAsync(ViewNotePage.UrlRef);
        return new ViewNotePage(_page);
    }

    public async Task<bool> IsTestNoteVisible()
    {
        return await _lnkTestNote.IsVisibleAsync();
    }

    public async Task<ScholarPersonalInfoPage> ClickPersonalInfoTabAsync()
    {
        await _lnkPersonalInfo.ClickAsync();
        await _page.WaitForURLAsync(ScholarPersonalInfoPage.UrlRef);
        return new ScholarPersonalInfoPage(_page);
    }

    public async Task<ScholarTaskCenterPage> ClickTasksTabAsync()
    {
        await _lnkTasks.ClickAsync();
        await _page.WaitForURLAsync(ScholarTaskCenterPage.UrlRef);
        return new ScholarTaskCenterPage(_page);
    }

    public async Task<ScholarAcademicsPage> ClickAcademicsTabAsync()
    {
        await _lnkAcademics.ClickAsync();
        await _page.WaitForURLAsync(ScholarAcademicsPage.UrlRef);
        return new ScholarAcademicsPage(_page);
    }
}