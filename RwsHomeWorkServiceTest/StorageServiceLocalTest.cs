using NUnit.Framework;
using RwsHomeworkService.StorageService;
using RwsHomeworkService.StorageService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RwsHomeWorkServiceTest
{
    public class Tests
    {
        private StorageService _storageService;

        [SetUp]
        public void Setup()
        {
            _storageService = new StorageService(new StorageServiceLocal());
        }

        /// <summary>
        /// Try to download no-existing file - must ends with error "FileDontExist"
        /// </summary>
        [Test]
        public void DownloadDataLocalTestNoExistingFile()
        {
            string sourceFileName = "this is no-existing file name, must fail";
            FileResult fileRes = _storageService.GetFileBytes(sourceFileName);
            bool res = fileRes.Errors.ErrorList.Select(x => x.ErrorId == RwsHomeworkService.StorageService.Enums.FileSystemErrorEnum.FileDontExist).Any();
            Assert.IsTrue(res);
        }

        /// <summary>
        /// Try to download existing test file, must be ok
        /// </summary>
        [Test]
        public void DownloadDataLocalTestFile()
        {
            string sourceFileName = Path.Combine(Environment.CurrentDirectory, "SourceFiles\\TestFileDocument1.xml");
            FileResult fileRes = _storageService.GetFileBytes(sourceFileName);
            bool res = fileRes.Errors.Status && fileRes.FileBytes.Length == 160;
            Assert.IsTrue(res);
        }
    }
}