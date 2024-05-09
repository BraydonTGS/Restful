﻿using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Restful.Core.Services
{
    public class FileExportService : IFileExportService
    {
        #region ChooseExportFilePath
        /// <summary>
        /// Choose the Specified Export File Path
        /// </summary>
        /// <returns></returns>
        public string ChooseExportFilePath()
        {
            try
            {

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Save File",
                    Filter = "Text Files (*.txt)|*.txt|JSON Files (*.json)|*.json|All files (*.*)|*.*",
                    DefaultExt = "txt"
                };

                if (saveFileDialog.ShowDialog()== DialogResult.OK) return saveFileDialog.FileName;

                return string.Empty;
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region ExportJsonStringToFile
        /// <summary>
        /// Exports a JSON string to a specified file location.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ExportJsonStringToFile(string json, string location)
        {
            try
            {
                var directory = Path.GetDirectoryName(location);
                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

                File.WriteAllText(location, json, Encoding.UTF8);

                return true;
            }
            catch (Exception) { throw; }
        }
        #endregion

    }
}
