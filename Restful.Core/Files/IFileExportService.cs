﻿namespace Restful.Core.Files
{
    /// <summary>
    /// Interface to Define Methods for the <see cref="FileExportService"/>
    /// </summary>
    public interface IFileExportService
    {
        string ChooseExportFilePath();
        bool ExportJsonStringToFile(string json, string location);
    }
}