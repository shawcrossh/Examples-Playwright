using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Admin;

public class ScholarDocumentsUploadPage
{
    private readonly IPage _page;
    private readonly ILocator _btnUpload;
    private readonly ILocator _lstType;
    private readonly ILocator _btnAddFiles;

    public const string UrlRef = "**/ScholarFiles/Upload/**";

    public ScholarDocumentsUploadPage(IPage page)
    {
        _page = page;
        _btnUpload = _page.GetByRole(AriaRole.Button, new() { NameString = "Upload" });
        _lstType = _page.Locator("#FileType");
        _btnAddFiles = _page.GetByRole(AriaRole.Button, new() { NameString = "Drop files here to upload" });
    }

    public async Task SelectUploadTypeAsync(string type)
    {
        await _lstType.SelectOptionAsync(new SelectOptionValue() { Label = type });
    }

    public async Task SetUploadFileAsync(string filename)
    {
        //open file chooser
        var fileChooser = await _page.RunAndWaitForFileChooserAsync(async () => await _btnAddFiles.ClickAsync());
        //set file we have to upload
        await fileChooser.SetFilesAsync(filename);
    }

    //performs upload prep actions in one call
    public async Task SetUploadFileAsync(string type, string filename)
    {
        await SelectUploadTypeAsync(type);
        await SetUploadFileAsync(filename);
    }

    public async Task<ScholarDocumentsPage> ClickUploadAsync()
    {
        await _btnUpload.ClickAsync();
        await _page.WaitForURLAsync(ScholarDocumentsPage.UrlRef);
        return new ScholarDocumentsPage(_page);
    }
}