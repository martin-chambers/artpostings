using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;

namespace ArtPostings.Models
{
    public class ItemPosting
    {
        private string webSafePictureFolder = ConfigurationManager.AppSettings["pictureLocation"];
        public int Id { get; set; }
        public int Order { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ShortName { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Header { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Size { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Price { get; set; }
        public bool Archive_Flag { get; set; }
        public ItemPosting(string id, string order, string filepath, string filename, string title, string shortName, string header, string description, string size, string price, string archive_flag)
        {
            Id = Convert.ToInt32(id);
            FileName = HttpUtility.UrlPathEncode(filename);
            Order = Convert.ToInt32(order);
            // The official advice is not to use UrlPathEncode because it does not escape all alphanumerics. Trouble
            // is - the src attribute of the img tag requires a vanilla string with single quotes and space chararcters where
            // they appear in the filename. Even if you use UrlDecode in the view, UrlEncode replaces space chararcters with 
            // '+' symbols, which seem to be converted back to breaking space characters by UrlDecode
            //FilePath = HttpUtility.UrlEncode(Path.Combine(webSafePictureFolder, filename).WithNBSP());
            FilePath = HttpUtility.UrlPathEncode(Path.Combine(webSafePictureFolder, filename));
            // title is passed to jQuery as Html, so need to avoid breaking spaces
            Title = title.WithNBSP();
            Header = header;
            Description = description;
            Size = size;
            Price = price;
            ShortName = shortName.Replace(" ", "_");
            Archive_Flag = Convert.ToBoolean(archive_flag);
        }
        /// <summary>
        /// constructor with minimal parameters for basic use
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="archived"></param>
        public ItemPosting(string filename, bool archived = true)
        {
            FileName = filename;
            FilePath = Path.Combine(webSafePictureFolder, filename);
            Archive_Flag = archived;
            Description = "";
            Header = FileName;
            Order = 0;
            Price = "";
            ShortName = FileName;
            Size = "";
            Title = FileName;
        }
        
        /// <summary>
        /// default constructor
        /// </summary>
        public ItemPosting() { }        
    }    
}