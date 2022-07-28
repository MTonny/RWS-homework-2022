using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Enums
{
    public enum FileSystemErrorEnum
    {
        // Requested file dont exist
        FileDontExist = 100,

        // Trying uploud existing file
        FileExistYet = 101,

        // Bad file format ( for example bad conversion )
        BadFileFormat = 102,

        // Error during saving
        ErrorDuringSaving = 103,

        // Error during loading
        ErrorDuringLoading = 104,

        // Error during file type converting
        ErrorDuringTypeConvert = 105,              
        
    }
}
