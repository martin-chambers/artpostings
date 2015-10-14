using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtPostings.Models
{
    public class ItemPosting
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }
        // For future reference: 
        // I think a ItemPostingViewModel class is required - consisting perhaps of 
        // an ItemPosting and an edit flag
        public bool Editing { get; set; }
        public ItemPosting(string id, string filepath, string title, string shortName, string header, string description, string size, string price)
        {
            Id = Convert.ToInt32(id);
            FilePath = HttpUtility.UrlPathEncode(filepath);
            // title is passed to jQuery as Html, so need to avoid breaking spaces
            Title = title.Replace(" ", "&nbsp");
            Header = header;
            Description = description;
            Size = size;
            Price = price;
            ShortName = shortName.Replace(" ", "_");
            Editing = false;
        }
        // default constructor
        public ItemPosting() { }
        
    }    
}