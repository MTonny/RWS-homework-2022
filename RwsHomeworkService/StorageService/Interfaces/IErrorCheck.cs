using RwsHomeworkService.StorageService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Interfaces
{
    /// <summary>
    /// Interface for error check definitions
    /// </summary>
    public interface IErrorCheck
    {
        List<Error> ErrorCheck();
    }
}
