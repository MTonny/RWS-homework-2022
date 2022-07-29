using RwsHomeworkService.StorageService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Interfaces
{
    /// <summary>
    /// Interface for file storage definitions
    /// </summary>
    public interface IFileSystem
    {
        FileResult GetFileBytes(string fullFileName);

        FileResult UploadFileBytes(byte[] fileBytes, string fullFileName);

        FileResult DeleteFile(string fullFileName);

    }
}
