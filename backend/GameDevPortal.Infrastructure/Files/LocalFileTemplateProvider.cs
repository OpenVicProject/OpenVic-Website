using GameDevPortal.Core.Interfaces.Notifications;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;

namespace GameDevPortal.Infrastructure.Files;

public class LocalFileTemplateProvider : ITemplateProvider
{
    private readonly string _templateFolderPath;

    public LocalFileTemplateProvider(string templateFolderPath = "")
    {
        _templateFolderPath = templateFolderPath;
        if (!templateFolderPath.IsNullOrEmpty() && !FileSystem.DirectoryExists(templateFolderPath))
        {
            throw new FileNotFoundException("Template folder not found.", templateFolderPath);
        }
    }

    public string ReadTemplate(string templateName)
    {
        string templatePath = Path.Combine(_templateFolderPath, templateName);

        if (!FileSystem.FileExists(templatePath))
        {
            throw new FileNotFoundException("Template file not found.", templatePath);
        }

        using StreamReader reader = new(templatePath);

        return reader.ReadToEnd();
    }
}