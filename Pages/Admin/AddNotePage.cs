using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class AddNotePage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _optContactNo;
    private readonly ILocator _lstCategory;
    private readonly ILocator _txtSummary;
    private readonly ILocator _txtNote;
    private readonly ILocator _lstTimeSpent;
    private readonly ILocator _btnSave;

    public const string UrlRef = "**/ScholarNotes/AddNote/**";

    public AddNotePage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _optContactNo = _page.GetByRole(AriaRole.Radio, new() { NameString = "No" });
        _lstCategory = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Category" });
        _txtSummary = _page.GetByLabel("Summary");
        _txtNote = _page.FrameLocator("#Note_ifr").Locator("#tinymce");
        _lstTimeSpent = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Time Spent" });
        _btnSave = _page.GetByRole(AriaRole.Button, new() { NameString = "Save" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task ClickContactNoAsync() => await _optContactNo.ClickAsync();

    public async Task SetCategoryAsync(string category) => await _lstCategory.SelectOptionAsync(new SelectOptionValue() { Label = category });

    public async Task SetSummaryAsync(string summary) => await _txtSummary.FillAsync(summary);

    public async Task SetNoteAsync(string note) => await _txtNote.FillAsync(note);

    public async Task SetTimeSpentAsync(string timeSpent) => await _lstTimeSpent.SelectOptionAsync(new SelectOptionValue() { Label = timeSpent });

    public async Task<ScholarProfilePage> ClickSaveAsync()
    {
        await _btnSave.ClickAsync();
        await _page.WaitForURLAsync(ScholarProfilePage.UrlRef);
        return new ScholarProfilePage(_page);
    }
}