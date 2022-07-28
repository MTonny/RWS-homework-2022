using RwsHomeworkService.StorageService.Interfaces;
using RwsHomeworkService.StorageService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService
{
    public class StorageServiceCloud : IFileSystem
    {
        public FileResult DeleteFile(string fullFileName)
        {
            throw new NotImplementedException();
        }

        public FileResult GetFileBytes(string fullFileName)
        {
            throw new NotImplementedException();
        }

        public FileResult UploadFileBytes(byte[] fileBytes, string fullFileName)
        {
            throw new NotImplementedException();
        }
    }
}
