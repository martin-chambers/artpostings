using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnnChambersArt.Models
{
    public class ItemPosting
    {
        public string FilePath { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }
        public ItemPosting(string filepath, string title, string shortName, string header, string description, string size, string price)
        {
            FilePath = HttpUtility.UrlPathEncode(filepath);
            Title = title.Replace(" ", "&nbsp");
            ShortName = shortName;
            Header = header;
            Description = description;
            Size = size;
            Price = price;
        }
        
    }    
}