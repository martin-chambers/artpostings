using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtPostings.Models
{
    public class ItemPostingViewModel
    {
        public bool Editing { get; set; }
        public ItemPosting Posting {get; set;}

    }
}