using RwsHomeworkService.StorageService.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Helpers
{
    /// <summary>
    /// Helper for enums
    /// </summary>
    public class FileSystemErrorEnumHelper
    {
        /// <summary>
        /// Get string note for errorEnum
        /// </summary>
        /// <param name="errorEnum"></param>
        /// <returns></returns>
        public static string GetErrorNote(FileSystemErrorEnum errorEnum)
        {
            switch (errorEnum)
            {                
                case FileSystemErrorEnum.FileDontExist:
                    return "Requested file does not exist.";
                case FileSystemErrorEnum.FileExistYet:
                    return "Uploading file already exists.";
                case FileSystemErrorEnum.BadFileFormat:
                    return "Bad file format conversion combination.";
                case FileSystemErrorEnum.ErrorDuringSaving:
                    return "Error during saving.";
                case FileSystemErrorEnum.ErrorDuringLoading:
                    return "Error during loading.";
                case FileSystemErrorEnum.ErrorDuringTypeConvert:
                    return "Error during data format converting.";
                    
            }

            return "undefined";
        }        
    }
}
