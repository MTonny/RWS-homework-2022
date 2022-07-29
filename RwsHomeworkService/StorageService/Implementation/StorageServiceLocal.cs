using RwsHomeworkService.StorageService.Enums;
using RwsHomeworkService.StorageService.Implementation.Checks.StorageLocal;
using RwsHomeworkService.StorageService.Interfaces;
using RwsHomeworkService.StorageService.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService
{
    public class StorageServiceLocal : IFileSystem
    {
        public FileResult DeleteFile(string fullFileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get file bytes from storage
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public FileResult GetFileBytes(string fullFileName)
        {
            FileResult res = new FileResult();

            // Check if process can continue
            StorageServiceCheck storageServiceCheck = new StorageServiceCheck();
            storageServiceCheck.AddCheck(new CheckStorageLocalFileExists(StorageServiceOperationTypeEnum.Download, fullFileName));

            ErrorResult storageErrors = storageServiceCheck.Check();
            if (!storageErrors.Status)
            {
                res.Errors.ErrorList.AddRange(storageErrors.ErrorList);
                return res;
            }          
            
            // Get bytes
            try
            {
                System.IO.FileStream fs = System.IO.File.OpenRead(fullFileName);
                byte[] data = new byte[fs.Length];
                int byteCount = fs.Read(data, 0, data.Length);
                if (byteCount != fs.Length)
                {
                    res.Errors.ErrorList.Add(new Error(Enums.FileSystemErrorEnum.ErrorDuringLoading, StorageServiceTypeEnum.Local));
                }
                fs.Close();
                fs.Dispose();

                res.FileBytes = data;
                res.FileName = fullFileName;
                return res;
            }
            catch (Exception ex)
            {
                // log ex
                throw ex;
            }
        }

        /// <summary>
        /// Upload file to storage
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public FileResult UploadFileBytes(byte[] fileBytes, string fullFileName)
        {
            FileResult res = new FileResult();

            // Check if process can continue
            StorageServiceCheck storageServiceCheck = new StorageServiceCheck();
            storageServiceCheck.AddCheck(new CheckStorageLocalFileExists(StorageServiceOperationTypeEnum.Upload, fullFileName));

            ErrorResult storageErrors = storageServiceCheck.Check();
            if (!storageErrors.Status)
            {
                res.Errors.ErrorList.AddRange(storageErrors.ErrorList);
                return res;
            }

            // Upload file
            try
            {
                System.IO.FileStream fs = System.IO.File.OpenWrite(fullFileName);

                fs.Write(fileBytes, 0, fileBytes.Length);

                int byteCount = fileBytes.Length;
                if (byteCount != fs.Length)
                {
                    res.Errors.ErrorList.Add(new Error(Enums.FileSystemErrorEnum.ErrorDuringSaving, StorageServiceTypeEnum.Local));
                }
                fs.Close();
                fs.Dispose();
                               
                return res;
            }
            catch (Exception ex)
            {
                // log ex
                throw ex;
            }
        }
    }
}
