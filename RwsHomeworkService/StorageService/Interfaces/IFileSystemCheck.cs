using RwsHomeworkService.StorageService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Interfaces
{
    /// <summary>
    /// Interface for checking service definitions
    /// </summary>
    public interface IFileSystemCheck
    {
        ErrorResult Check();
        void AddCheck(IErrorCheck errorCheck);
        void RemoveCheck(IErrorCheck errorCheck);
    }
}
