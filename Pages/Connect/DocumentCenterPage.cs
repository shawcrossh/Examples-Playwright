using Microsoft.Playwright;

namespace Example.ExampleLeaders.Automation.Pages.Connect;

public class DocumentCenterPage
{
    private readonly IPage _page;
    private readonly ILocator _btnUpload;
    private readonly ILocator _lstType;
    private readonly ILocator _btnAddFiles;

    private const string SuccessSelector = "div.green-message-box";

    public const string UrlRef = "**/DocumentCenter";

    public DocumentCenterPage(IPage page)
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

    public async Task ClickUploadAndWaitForResponseAsync()
    {
        await _btnUpload.ClickAsync();
        await _page.WaitForSelectorAsync(SuccessSelector, new() { State = WaitForSelectorState.Visible });
    }
}