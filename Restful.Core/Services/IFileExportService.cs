namespace Restful.Core.Services
{
    public interface IFileExportService
    {
        string ChooseExportFilePath();
        bool ExportJsonStringToFile(string json, string location);
    }
}