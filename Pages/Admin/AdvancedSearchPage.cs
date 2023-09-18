using Microsoft.Playwright;
using System.Reflection.Metadata.Ecma335;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class AdvancedSearchPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogout;
    private readonly ILocator _chkActive;
    private readonly ILocator _btnSearch;
    private readonly ILocator _tblSearchResults;
    private readonly ILocator _lnkFirstPerson;
    private readonly ILocator _lnkAcademic;
    private readonly ILocator _lstUniversity;
    private readonly ILocator _txtName;

    public readonly ILocator FirstStatus;
    public readonly ILocator FirstUniversity;

    public const string UrlRef = "**/ScholarSearch/Advanced";

    public AdvancedSearchPage(IPage page)
    {
        _page = page;
        _lnkLogout = _page.GetByRole(AriaRole.Link, new() { NameString = "Log Out" });
        _chkActive = _page.Locator("#Form_ScholarStatusOption_2");
        _lnkAcademic = _page.Locator("#academic_link");
        _lstUniversity = _page.Locator("#Form_CollegeId");
        _txtName = _page.GetByRole(AriaRole.Textbox, new() { NameString = "Name", Exact = true });
        _btnSearch = _page.Locator("#search_button");
        _tblSearchResults = _page.GetByRole(AriaRole.Grid);
        //body of table, first row, 4th cell is the name+link
        _lnkFirstPerson = _tblSearchResults.Locator("tbody tr:nth-child(1) td:nth-child(4)").GetByRole(AriaRole.Link).First;
        //public, same row as first person but cells to the right
        FirstStatus = _tblSearchResults.Locator("tbody tr:nth-child(1) td:nth-child(6)");
        FirstUniversity = _tblSearchResults.Locator("tbody tr:nth-child(1) td:nth-child(7)");
    }

    public ILocator FirstPerson => _lnkFirstPerson;

    public async Task ClickLogoutAsync() => await _lnkLogout.ClickAsync();

    public async Task ClickActiveAsync() => await _chkActive.ClickAsync();

    public async Task FilterOnUniversity(string value)
    {
        await _lnkAcademic.ClickAsync();
        await _lstUniversity.SelectOptionAsync(new SelectOptionValue() { Label = value });
    }

    public async Task<ScholarProfilePage> FindAndSelectFirstActiveScholarAsync()
    {
        await _chkActive.ClickAsync();
        await _btnSearch.ClickAsync();
        await _tblSearchResults.IsVisibleAsync();
        await _lnkFirstPerson.ClickAsync();
        await _page.WaitForURLAsync(ScholarProfilePage.UrlRef);
        return new ScholarProfilePage(_page);
    }

    public async Task SearchActive()
    {
        await _chkActive.ClickAsync();
        await _btnSearch.ClickAsync();
        await _tblSearchResults.IsVisibleAsync();
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task SearchActiveByName(string name)
    {
        await _chkActive.ClickAsync();
        await _txtName.FillAsync(name);
        await _btnSearch.ClickAsync();
        await _tblSearchResults.IsVisibleAsync();
        //is really slow, this will wait for page to finish loading*
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }
}