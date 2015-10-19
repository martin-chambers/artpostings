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
        IEnumerable<ItemPosting> EditModeShopPostings(int id);
        IEnumerable<ItemPosting> EditModeArchivePostings(int id);
        ItemPosting GetPosting(int id);
        string ArchiveText { get; }
        string ShopText { get; }
    }
}
