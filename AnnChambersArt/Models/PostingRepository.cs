using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnnChambersArt.Models
{
    public class PostingRepository : IPostingRepository
    {
        bool IPostingRepository.Create(ItemPosting posting)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ItemPosting> IPostingRepository.ListPostings()
        {
            //throw new NotImplementedException();
            return new List<ItemPosting>()
            {
                new ItemPosting(
                    "Pasquale.jpg", 
                    "Pasquale", 
                    "Pasquale", 
                    "Pasquale", 
                    "An abstract painting . Acrylic paint on canvas.", 
                    "102 cm x 80 cm x 15 mm", 
                    "£300"),
                new ItemPosting(
                    "Tanyas waterlilies.jpg", 
                    "Tanya's Waterlilies (19)", 
                    "Tanya's Waterlilies (19)", 
                    "Tanya's Waterlilies (19)", 
                    "Print of an abstract acrylic painting. Can be framed.", 
                    "60 x 46 x 4cm", 
                    "£120 not framed")
            };
        }
    }
}