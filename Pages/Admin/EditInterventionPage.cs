using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class EditInterventionPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _btnSubmit;
    private readonly ILocator _btnAddMilestone;
    private readonly ILocator _txtDescription;
    private readonly ILocator _lstType;
    private readonly ILocator _txtMilestoneDueDate;
    private readonly ILocator _txtDoneDate;
    private readonly ILocator _txtMilestoneDoneDate;
    private readonly ILocator _lstInterventionType;
    private readonly ILocator _lstStatus;
    private readonly ILocator _txtActualOutcome;
    private readonly ILocator _lstStudentEngagement;
    //private readonly ILocator _sldRating;

    public const string UrlRef = "**/ScholarInterventions/Edit/**";

    public EditInterventionPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _btnSubmit = _page.GetByRole(AriaRole.Button, new() { NameString = "Submit" });
        _btnAddMilestone = _page.GetByRole(AriaRole.Button, new() { NameString = "Add" });
        //these simple locators fail if there are multiple goals
        //_txtDescription = _page.GetByLabel("Description *");
        //_txtDueDate = _page.GetByLabel("Due Date *"); //also renamed to get more specific
        //_txtMilestoneDoneDate = _page.GetByRole(AriaRole.Textbox, new() { NameString = "Done Date" });
        _txtDoneDate = _page.GetByLabel("Done Date *", new() { Exact = true });
        _txtDescription = _page.Locator("input[name=\"milestones\\[0\\]\\.description\"]");
        _txtMilestoneDueDate = _page.Locator("input[name=\"milestones\\[0\\]\\.dueDate\"]");
        _txtMilestoneDoneDate = _page.Locator("input[name=\"milestones\\[0\\]\\.doneDate\"]");  //added to cover ending vs milestone
        _lstType = _page.Locator("select[name=\"milestones\\[0\\]\\.type\"]");
        _lstInterventionType = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Intervention Type *" });
        _lstStatus = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Status *" });
        _txtActualOutcome = _page.GetByRole(AriaRole.Textbox, new() { NameString = "Actual Outcome *" });
        _lstStudentEngagement = _page.GetByRole(AriaRole.Combobox, new() { NameString = "Student Engagement *" });
        //_sldRating = _page.GetByRole(AriaRole.Slider, new() { NameString = "rating" });
        //_sldRating = _page.Locator("#OutcomeRating");
    }

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task ClickAddMilestoneAsync()
    {
        await _lstInterventionType.IsVisibleAsync();
        await _btnAddMilestone.ClickAsync();
        await _txtDescription.IsVisibleAsync();
    }

    public async Task AddMilestoneInfoAsync(string description, string type, string date)
    {
        await _txtDescription.FillAsync(description);
        await _lstType.SelectOptionAsync(new SelectOptionValue() { Label = type });
        await _txtMilestoneDueDate.FillAsync(date);
        await _txtMilestoneDoneDate.FillAsync(date);
    }

    public async Task EndInterventionAsync(string doneDate, string engagement, string description)
    {
        await _lstInterventionType.IsVisibleAsync();
        await _lstStatus.SelectOptionAsync(new SelectOptionValue() { Label = "Ended" });
        await _txtActualOutcome.FillAsync(description);
        await _txtDoneDate.FillAsync(doneDate);
        await _lstStudentEngagement.SelectOptionAsync(new SelectOptionValue() { Label = engagement });
        //await _sldRating.FillAsync("5");
    }

    public async Task<InterventionsPage> ClickSubmitAsync()
    {
        await _btnSubmit.ClickAsync();
        await _page.WaitForURLAsync(InterventionsPage.UrlRef);
        return new InterventionsPage(_page);
    }
}