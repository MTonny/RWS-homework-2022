using Newtonsoft.Json;
using RwsHomeworkService.StorageService.Enums;
using RwsHomeworkService.StorageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RwsHomeworkService.ConversionService
{
    public class ConversionService
    {
        // List defines allowed conversion
        private static readonly List<AllowedFileTypePAir> _AllowedConversionTypes = new List<AllowedFileTypePAir>()
        {            
            new AllowedFileTypePAir(){ Source = FileTypeEnum.JSON,         Target = FileTypeEnum.XML,      ConvertorFunctionDelegate = new AllowedFileTypePAir.ConvertorDelegate(ConvertJSONtoXML) },
            new AllowedFileTypePAir(){ Source = FileTypeEnum.XML,          Target = FileTypeEnum.JSON,     ConvertorFunctionDelegate = new AllowedFileTypePAir.ConvertorDelegate(ConvertXMLtoJSON) }
        };

        private static bool IsConversionAllowed(FileTypeEnum source, FileTypeEnum target)
        {
            return _AllowedConversionTypes.Where(x => x.Source == source && x.Target == target).Any();
        }

        public static FileResult ConvertData(FileTypeEnum inputType, FileTypeEnum targetType, byte[] fileBytes)
        {
            FileResult res = new FileResult();
            if (IsConversionAllowed(inputType, targetType))
            {
                var convertorFunctionDelegate = _AllowedConversionTypes.Where(x => x.Source == inputType && x.Target == targetType).Select(x => x.ConvertorFunctionDelegate).FirstOrDefault();
                if (convertorFunctionDelegate != null)
                {
                    try
                    {
                        res = convertorFunctionDelegate.Invoke(fileBytes);
                    }
                    catch (Exception ex)
                    {
                        // log ex
                        res.Errors.ErrorList.Add(new Error(FileSystemErrorEnum.ErrorDuringTypeConvert, StorageServiceTypeEnum.Local));
                    }
                }
                return res;
            }
            else
            {
                res.Errors.ErrorList.Add(new Error(FileSystemErrorEnum.BadFileFormat, StorageServiceTypeEnum.Local));
                return null;
            }

        }


        private static FileResult ConvertJSONtoXML(byte[] fileBytes)
        {
            FileResult res = new FileResult();
            
            string dataString = Encoding.UTF8.GetString(fileBytes);

            XNode node = JsonConvert.DeserializeXNode(dataString, "Root");
            string resFileBytesString = node.ToString();

            byte[] resBytes = System.Text.Encoding.UTF8.GetBytes(resFileBytesString);

            res.FileBytes = resBytes;

            return res;
        }

        private static FileResult ConvertXMLtoJSON(byte[] fileBytes)
        {
            FileResult res = new FileResult();     
           
            string dataString = Encoding.UTF8.GetString(fileBytes);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(dataString);

            string resFileBytesString = JsonConvert.SerializeXmlNode(doc);

            byte[] resBytes = System.Text.Encoding.UTF8.GetBytes(resFileBytesString);

            res.FileBytes = resBytes;

            return res;
        }

        internal class AllowedFileTypePAir
        {
            public delegate FileResult ConvertorDelegate(byte[] fileBytes);
            public FileTypeEnum Source { get; set; }
            public FileTypeEnum Target { get; set; }
            public ConvertorDelegate ConvertorFunctionDelegate { get; set; }
        }
    }    
}
