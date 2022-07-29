using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using RwsHomeworkService.StorageService;
using RwsHomeworkService.StorageService.Models;

namespace RwsHomework
{    
    public class Program
    {          
        static void Main(string[] args)
        {
            string sourceFileName = Path.Combine(Environment.CurrentDirectory, "SourceFiles\\Document1.xml");           
            string targetFileName = Path.Combine(Environment.CurrentDirectory, "TargetFiles\\Document1.json");

            // Get storage service
            StorageService storageService = new StorageService(new StorageServiceLocal());            
            FileResult inputFile;

            // Get file bytes
            try
            {                
                inputFile = storageService.GetFileBytes(sourceFileName);
            }
            catch (Exception ex)
            {
                // log ex
                throw ex;
            }

            // Getting proces ok
            if (inputFile.Errors.Status)
            {
                try
                {
                    var xdoc = XDocument.Parse(System.Text.Encoding.Default.GetString(inputFile.FileBytes));
                    var doc = new Document
                    {
                        Title = xdoc.Root.Element("title") != null ? xdoc.Root.Element("title").Value : "",
                        Text = xdoc.Root.Element("text") != null ? xdoc.Root.Element("text").Value : ""
                    };

                    // Create JSON string
                    string serializedDoc = JsonConvert.SerializeObject(doc);
                    byte[] serializedDocBytes = System.Text.Encoding.UTF8.GetBytes(serializedDoc);

                    // Upload new file
                    FileResult resUpload = storageService.UploadFileBytes(serializedDocBytes, targetFileName);
                    if (!resUpload.Errors.Status)
                    {
                        PrintErrors(resUpload.Errors.ErrorList);
                        Console.WriteLine("\u001b[31mprogram ends with errors\u001b[0m");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // log ex
                    throw ex;
                }                
            }
            else
            {
                PrintErrors(inputFile.Errors.ErrorList);
                Console.WriteLine("\u001b[31mprogram ends with errors\u001b[0m");
                return;
            }
            Console.WriteLine("\u001b[32mDone!\u001b[0m");
        }

        /// <summary>
        /// Prints errors
        /// </summary>
        /// <param name="errors">List of errors</param>
        private static void PrintErrors(List<Error> errors)
        {
           errors.ForEach(x =>
           {
               Console.WriteLine(String.Format("\u001b[33merror ID:\u001b[0m {0}, \u001b[33mstorage:\u001b[0m {1}, \u001b[33mmessage:\u001b[0m {2}", x.ErrorId, x.StorageType, x.Message));
           });
           Console.WriteLine();
        }

        internal class Document
        {
            public string Title { get; set; }
            public string Text { get; set; }
        }
    }  
    
}