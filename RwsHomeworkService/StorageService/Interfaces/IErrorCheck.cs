using RwsHomeworkService.StorageService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Interfaces
{
    public interface IErrorCheck
    {
        List<Error> ErrorCheck();
    }
}
