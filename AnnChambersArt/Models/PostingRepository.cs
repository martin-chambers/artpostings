using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnnChambersArt.Models
{
    public class PostingRepository : IPostingRepository
    {
        // this won't stay here
        string filePath = "../../Content/art/";
        bool IPostingRepository.Create(ItemPosting posting)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ItemPosting> IPostingRepository.ShopPostings()
        {
            //throw new NotImplementedException();
            return new List<ItemPosting>()
            {
                new ItemPosting(
                    filePath + "Pasquale.jpg",                     
                    "Pasquale", 
                    "Pasquale", 
                    "Pasquale", 
                    "An abstract painting . Acrylic paint on canvas.", 
                    "102 cm x 80 cm x 15 mm", 
                    "£300"),
                new ItemPosting(
                    filePath + "Tanyas waterlilies.jpg", 
                    "Tanya's Waterlilies (19)", 
                    "Tanya's Waterlilies (19)", 
                    "Tanya's Waterlilies (19)", 
                    "Print of an abstract acrylic painting. Can be framed.", 
                    "60 x 46 x 4cm", 
                    "£120 not framed"),
                new ItemPosting(
                    filePath + "Blue Cow.jpg", 
                    "Blue Cow", 
                    "Blue Cow", 
                    "Blue Cow", 
                    "Print of an abstract acrylic painting. Can be framed.", 
                    "60 x 46 x 4cm", 
                    "£120 not framed")
            };
        }
        IEnumerable<ItemPosting> IPostingRepository.ArchivePostings()
        {
            //throw new NotImplementedException();
            return new List<ItemPosting>()
            {
                new ItemPosting(
                    filePath + "Paprika.jpg",                     
                    "Paprika", 
                    "Paprika", 
                    "Paprika", 
                    "An abstract painting . Acrylic paint on a deep profile canvas.", 
                    "102 x 76 x 4cm", 
                    "(no longer for sale)"),
                new ItemPosting(
                    filePath + "Golden Storm.jpg", 
                    "Golden Storm", 
                    "Golden Storm", 
                    "Golden Storm", 
                    "Impressionist painting. Acrylic paint on canvas with a 15 mm profile.", 
                    "101 x 80 cm", 
                    "(no longer for sale)")
            };
        }
    }
}