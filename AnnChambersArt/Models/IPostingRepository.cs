using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnChambersArt.Models
{
    public interface IPostingRepository
    {
        bool Create(ItemPosting posting);
        IEnumerable<ItemPosting> ListPostings();        
    }
}
