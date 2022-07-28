using System;
using System.Collections.Generic;
using System.Text;

namespace RwsHomeworkService.StorageService.Models
{
    public class FileResult
    {
        public ErrorResult Errors { get; set; } = new ErrorResult();
        public byte[] FileBytes { get; set; }

        public string FileName { get; set; }

    }
}
