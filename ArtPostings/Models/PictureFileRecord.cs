using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace ArtPostings.Models
{
    public class PictureFileRecord
    {
        private string webSafePictureFolder = ConfigurationManager.AppSettings["pictureLocation"];
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool Displayed{ get; set; }
        public bool Archived { get; set; }
        public int Order { get; set; }
        public string Header { get; set; }
        public PictureFileRecord(string _filePath)
        {
            int posLastSlash = _filePath.LastIndexOf("\\");
            FileName = _filePath.Substring(posLastSlash + 1, _filePath.Length - (posLastSlash + 1));
            FilePath = HttpUtility.UrlPathEncode(Path.Combine(webSafePictureFolder, FileName));
            Displayed = false;
        }
    }
}