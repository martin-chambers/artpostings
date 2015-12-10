using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtPostings.Models
{
    public interface IPostingRepository
    {
        ChangeResult Create(ItemPosting posting, bool archive);
        ChangeResult Delete(ItemPosting posting);
        IEnumerable<ItemPosting> ShopPostings();
        IEnumerable<ItemPosting> ArchivePostings();
        ItemPosting GetPosting(int id);
        ItemPosting GetPosting(Predicate<ItemPosting> pred, bool archive);
        ChangeResult Update(ItemPosting posting, bool archived);
        ChangeResult ExchangeOrders(ItemPosting posting1, ItemPosting posting2, bool archived);
      }
}
