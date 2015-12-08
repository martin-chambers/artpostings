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
        List<PictureFileRecord> PictureFileRecordList(string mappedfolder, string status = "All");
        IEnumerable<PictureFileRecord> DeletePictureFile(string filename, string folder);
        ChangeResult InsertPosting(PictureFileRecord pfr, bool archive);
        ChangeResult RemoveFromDisplay(ItemPosting posting);
        ItemPostingViewModel CreateItemPostingViewModel(PictureFileRecord pfr);
        ChangeResult SaveShopChanges(ItemPostingViewModel vm);
        ChangeResult SaveArchiveChanges(ItemPostingViewModel vm);
        string ArchiveText { get; }
        string ShopText { get; }
        string FullyMappedPictureFolder { get; }

    }
}
