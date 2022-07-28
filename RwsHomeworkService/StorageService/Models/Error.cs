using RwsHomeworkService.StorageService.Enums;
using RwsHomeworkService.StorageService.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Models
{
    public class Error
    {  
        public FileSystemErrorEnum ErrorId { get; set; }
        public StorageServiceTypeEnum StorageType { get; set; }
        public string Message { get; set; }        

        public Error(FileSystemErrorEnum errorId, StorageServiceTypeEnum storageType)
        {
            ErrorId = errorId;
            StorageType = storageType;
            Message = FileSystemErrorEnumHelper.GetErrorNote(errorId);
        }

    }
}
