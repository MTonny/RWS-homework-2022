using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Models
{
    public class ErrorResult
    {
        // If true - OK, no errors
        public bool Status
        {
            get { return ErrorList.Count == 0; }
        }

        public List<Error> ErrorList { get; set; } = new List<Error>();
    }
}
