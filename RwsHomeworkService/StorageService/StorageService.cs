using RwsHomeworkService.StorageService.Interfaces;
using RwsHomeworkService.StorageService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService
{
    public class StorageService : IFileSystem
    {
        private IFileSystem _IFileSystem;
        public StorageService(IFileSystem iFileSystem)
        {
            _IFileSystem = iFileSystem;
        }

        public FileResult DeleteFile(string fullFileName)
        {
            return _IFileSystem.DeleteFile(fullFileName);
        }

        public FileResult GetFileBytes(string fullFileName)
        {
            return _IFileSystem.GetFileBytes(fullFileName);
        }

        public FileResult UploadFileBytes(byte[] fileBytes, string fullFileName)
        {
            return _IFileSystem.UploadFileBytes(fileBytes, fullFileName);
        }
    }
}
