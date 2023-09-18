using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ScholarTaskCreatePage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _btnSave;
    private readonly ILocator _lstAssignedTo;
    private readonly ILocator _lstCategory;
    private readonly ILocator _lstTaskType;
    private readonly ILocator _txtTitle;
    private readonly ILocator _txtDescription;
    private readonly ILocator _txtDueOn;

    public const string UrlRef = "**/Tasks/Create/**";

    public ScholarTaskCreatePage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _btnSave = _page.GetByRole(AriaRole.Button, new() { NameString = "Create Task" });
        _lstAssignedTo = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Assigned To:*" });
        _lstCategory = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Category:*" });
        _lstTaskType = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Task Type:*" });
        _txtTitle = _page.GetByRole(AriaRole.Textbox, new() { NameString = "Title:*" });
        _txtDescription = _page.GetByRole(AriaRole.Textbox, new() { NameString = "Description:*" });
        _txtDueOn = _page.GetByRole(AriaRole.Textbox, new() { NameString = "Due On:*" });
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task<ScholarTaskCenterPage> ClickSaveAsync()
    {
        await _btnSave.ClickAsync();
        await _page.WaitForURLAsync(ScholarTaskCenterPage.UrlRef);
        return new ScholarTaskCenterPage(_page);
    }

    public async Task CreateStudentQuestionTask(string title, string description, string dueOn)
    {
        await _lstAssignedTo.SelectOptionAsync(new SelectOptionValue() { Label = "Student" });
        await _lstAssignedTo.SelectOptionAsync(new SelectOptionValue() { Label = "General" });
        await _lstTaskType.SelectOptionAsync(new SelectOptionValue() { Label = "Question" });
        await _txtTitle.FillAsync(title);
        await _txtDescription.FillAsync(description);
        await _txtDueOn.FillAsync(dueOn);
    }
}