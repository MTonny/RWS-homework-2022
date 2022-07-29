using RwsHomeworkService.StorageService.Enums;
using RwsHomeworkService.StorageService.Interfaces;
using RwsHomeworkService.StorageService.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Implementation.Checks.StorageLocal
{
    /// <summary>
    /// Check definition for StorageLocal
    /// Type: check file exists
    /// </summary>
    public class CheckStorageLocalFileExists : IErrorCheck
    {
        private StorageServiceOperationTypeEnum _operationType;
        private string _fullFileName;
        private bool _canRewriteFile;

        public CheckStorageLocalFileExists(StorageServiceOperationTypeEnum operationType, string fullFileName, bool canRewriteFile = false)
        {
            _operationType = operationType;
            _fullFileName = fullFileName;
            _canRewriteFile = canRewriteFile;
        }

        public List<Error> ErrorCheck()
        {
            List<Error> res = new List<Error>();

            bool fileExists = File.Exists(_fullFileName);

            // File must exists
            if ((_operationType == StorageServiceOperationTypeEnum.Download) 
                || (_operationType == StorageServiceOperationTypeEnum.Remove) 
                || (_operationType == StorageServiceOperationTypeEnum.Rename))
            {
                if (!fileExists)
                {
                    res.Add(new Error(FileSystemErrorEnum.FileDontExist, StorageServiceTypeEnum.Local));
                }
            }
            // File must not exists
            else if (_operationType == StorageServiceOperationTypeEnum.Upload)
            {
                if (fileExists && !_canRewriteFile)
                {
                    res.Add(new Error(FileSystemErrorEnum.FileExistYet, StorageServiceTypeEnum.Local));
                }
            }
            else
            {
                throw new Exception("Bad function usage. Check your inputs.");
            }

            return res;
        }
    }
}
