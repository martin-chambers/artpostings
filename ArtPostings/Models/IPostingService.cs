using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtPostings.Models
{
    public interface IPostingService
    {
        IEnumerable<ItemPosting> ShopPostings();
        IEnumerable<ItemPosting> ArchivePostings();
        string ArchiveText { get; }
        string ShopText { get; }
    }
}
