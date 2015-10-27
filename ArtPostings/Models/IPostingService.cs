using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtPostings.Models
{
    public interface IPostingService
    {
        IEnumerable<ItemPostingViewModel> ShopPostings();
        IEnumerable<ItemPostingViewModel> ArchivePostings();
        IEnumerable<ItemPostingViewModel> EditModeShopPostings(int id);
        IEnumerable<ItemPostingViewModel> EditModeArchivePostings(int id);
        ItemPostingViewModel GetPosting(int id);
        List<PictureFileRecord> PictureFileRecordList();
        void SaveShopChanges(ItemPostingViewModel vm);
        void SaveArchiveChanges(ItemPostingViewModel vm);
        string ArchiveText { get; }
        string ShopText { get; }

    }
}
