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
            FileName = filename;
            Order = Convert.ToInt32(order);

            FilePath = HttpUtility.UrlPathEncode(Path.Combine(webSafePictureFolder, filename));
            // title is passed to jQuery as Html, so need to avoid breaking spaces
            Title = title.Replace(" ", "&nbsp");
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
        public ItemPosting(string filename, bool archived)
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