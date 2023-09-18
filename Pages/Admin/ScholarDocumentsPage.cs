using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ScholarDocumentsPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkUpload;
    private readonly ILocator _tblDocuments;
    private readonly ILocator _tblFirstRow;

    public const string UrlRef = "**/ScholarFiles/Index/**";

    public ScholarDocumentsPage(IPage page)
    {
        _page = page;
        _lnkUpload = _page.GetByRole(AriaRole.Link, new() { Name = "»Upload Documents" });
        _tblDocuments = _page.GetByRole(AriaRole.Grid);
        _tblFirstRow = _tblDocuments.Locator("tbody tr:nth-child(1)");
    }

    public ILocator FirstRow => _tblFirstRow;

    public async Task<ScholarDocumentsUploadPage> ClickUploadAsync()
    {
        await _lnkUpload.ClickAsync();
        await _page.WaitForURLAsync(ScholarDocumentsUploadPage.UrlRef);
        return new ScholarDocumentsUploadPage(_page);
    }
}