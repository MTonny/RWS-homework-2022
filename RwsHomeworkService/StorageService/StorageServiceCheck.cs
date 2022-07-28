using RwsHomeworkService.StorageService.Interfaces;
using RwsHomeworkService.StorageService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService
{
    public class StorageServiceCheck : IFileSystemCheck
    {
        private List<IErrorCheck> errorChecks = new List<IErrorCheck>();
        public void AddCheck(IErrorCheck errorCheck)
        {
            errorChecks.Add(errorCheck);
        }
        public void RemoveCheck(IErrorCheck errorCheck)
        {
            errorChecks.Remove(errorCheck);
        }
        public ErrorResult Check()
        {
           ErrorResult res = new ErrorResult();
           errorChecks.ForEach(x =>
           {
               res.ErrorList.AddRange(x.ErrorCheck());
           });
           
           return res;
        }        
    }
}
