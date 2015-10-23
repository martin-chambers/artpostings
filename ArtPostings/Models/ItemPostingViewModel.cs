using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtPostings.Models
{
    public class ItemPostingViewModel
    {
        public bool Editing { get; set; }
        // originally the below property was called 'Posting'. This caused a massively difficult problem to track down, because 
        // the JSON from the view was not received by the httppost controller, which used the 'posting' name as its 
        // ItemPostingViewModel parameter. The framework relies on property names to sort out some its binding issues
        public ItemPosting ItemPosting {get; set;}

    }
}