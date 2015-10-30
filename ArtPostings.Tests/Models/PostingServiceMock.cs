using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtPostings.Controllers;
using ArtPostings.Models;


namespace ArtPostings.Tests.Models
{
    class PostingServiceMock : IPostingService
    {
        public PostingServiceMock()
        {
            getAllPostings();
        }
        public string ArchiveText
        {
            get
            {
                return "This is Archive Text";
            }
        }
        public string FullyMappedPictureFolder
        {
            get { return ""; }

        }

        public string ShopText
        {
            get
            {
                return "This is Shop Text";
            }
        }
        private IEnumerable<ItemPostingViewModel> getAllPostings()
        {
            List<ItemPostingViewModel> postings = new List<ItemPostingViewModel>();
            postings.AddRange(this.ArchivePostings());
            postings.AddRange(this.ShopPostings());
            return postings;
        }

        public IEnumerable<ItemPostingViewModel> ArchivePostings()
        {
            List<ItemPostingViewModel> archivePostings = new List<ItemPostingViewModel>();
            archivePostings.Add(new ItemPostingViewModel()
            {
                ItemPosting = new ItemPosting("1",
                    "1",
                    "some filepath 1",
                    "some filename 1",
                    "some title 1",
                    "some shortname 1",
                    "some header 1",
                    "some description 1",
                    "some size 1",
                    "some price 1",
                    "true"),
                Editing = false
            });
            archivePostings.Add(new ItemPostingViewModel()
                { ItemPosting = new ItemPosting("2",
                    "2",
                    "some filepath 2",
                    "some filename 2",
                    "some title 2",
                    "some shortname 2",
                    "some header 2",
                    "some description 2",
                    "some size 2",
                    "some price 2",
                    "true"),
                Editing = false
            });
            archivePostings.Add(new ItemPostingViewModel()
            {
                ItemPosting = new ItemPosting("3",
                    "3",
                    "some filepath 3",
                    "some filename 3",
                    "some title 3",
                    "some shortname 3",
                    "some header 3",
                    "some description 3",
                    "some size 3",
                    "some price 3",
                    "true"),
                Editing = false
            });
            return archivePostings;
        }

        public IEnumerable<ItemPostingViewModel> EditModeArchivePostings(int id)
        {
            int editId = 2;
            List<ItemPostingViewModel> editModeArchivePostings = new List<ItemPostingViewModel>();
            editModeArchivePostings = (List<ItemPostingViewModel>)this.ArchivePostings();
            ItemPostingViewModel editModeItemPostingVM = editModeArchivePostings.Find(x => x.ItemPosting.Id == editId);
            editModeArchivePostings.RemoveAll(x => x.ItemPosting.Id == editId);
            editModeItemPostingVM.Editing = true;
            editModeArchivePostings.Add(editModeItemPostingVM);
            return editModeArchivePostings;
        }

        public IEnumerable<ItemPostingViewModel> EditModeShopPostings(int id)
        {
            int editId = 5;
            List<ItemPostingViewModel> editModeShopPostings = new List<ItemPostingViewModel>();
            editModeShopPostings = (List<ItemPostingViewModel>)this.ShopPostings();
            ItemPostingViewModel editModeItemPostingVM = editModeShopPostings.Find(x => x.ItemPosting.Id == editId);
            editModeShopPostings.RemoveAll(x => x.ItemPosting.Id == editId);
            editModeItemPostingVM.Editing = true;
            editModeShopPostings.Add(editModeItemPostingVM);
            return editModeShopPostings;
        }

        public ItemPostingViewModel GetPosting(int id)
        {
            List<ItemPostingViewModel> postings = this.getAllPostings().ToList();
            return postings.Find(x => x.ItemPosting.Id == id);
        }

        public IEnumerable<ItemPostingViewModel> ShopPostings()
        {
            List<ItemPostingViewModel> shopPostings = new List<ItemPostingViewModel>();
            shopPostings.Add(new ItemPostingViewModel()
            {
                ItemPosting = new ItemPosting("4",
                    "4",
                    "some filepath 4",
                    "some filename 4",
                    "some title 4",
                    "some shortname 4",
                    "some header 4",
                    "some description 4",
                    "some size 4",
                    "some price 4",
                    "false"),
                Editing = false
            });
            shopPostings.Add(new ItemPostingViewModel()
            {
                ItemPosting = new ItemPosting("5",
                    "5",
                    "some filepath 5",
                    "some filename 5",
                    "some title 5",
                    "some shortname 5",
                    "some header 5",
                    "some description 5",
                    "some size 5",
                    "some price 5",
                    "false"),
                Editing = false
            });
            shopPostings.Add(new ItemPostingViewModel()
            {
                ItemPosting = new ItemPosting("6",
                    "6",
                    "some filepath 6",
                    "some filename 6",
                    "some title 6",
                    "some shortname 6",
                    "some header 6",
                    "some description 6",
                    "some size 6",
                    "some price 6",
                    "false"),
                Editing = false
            });
            return shopPostings;
        }
        public void SaveArchiveChanges(ItemPostingViewModel vm)
        {
            throw new NotImplementedException();
        }
        public void SaveShopChanges(ItemPostingViewModel vm)
        {
            throw new NotImplementedException();
        }
        public List<PictureFileRecord> PictureFileRecordList(string mappedfolder, string status)
        {
            List<PictureFileRecord> pictureFiles = new List<PictureFileRecord>();
            return pictureFiles;
        }

        public IEnumerable<PictureFileRecord> DeletePictureFile(string filename, string folder)
        {
            throw new NotImplementedException();
        }

    }
}
