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
                Posting = new ItemPosting("1",
                    "some filepath 1",
                    "some title 1",
                    "some shortname 1",
                    "some header 1",
                    "some description 1",
                    "some size 1",
                    "some price 1"),
                Editing = false
            });
            archivePostings.Add(new ItemPostingViewModel()
                { Posting = new ItemPosting("2",
                    "some filepath 2",
                    "some title 2",
                    "some shortname 2",
                    "some header 2",
                    "some description 2",
                    "some size 2",
                    "some price 2"),
                Editing = false
            });
            archivePostings.Add(new ItemPostingViewModel()
            {
                Posting = new ItemPosting("3",
                    "some filepath 3",
                    "some title 3",
                    "some shortname 3",
                    "some header 3",
                    "some description 3",
                    "some size 3",
                    "some price 3"),
                Editing = false
            });
            return archivePostings;
        }

        public IEnumerable<ItemPostingViewModel> EditModeArchivePostings(int id)
        {
            int editId = 2;
            List<ItemPostingViewModel> editModeArchivePostings = new List<ItemPostingViewModel>();
            editModeArchivePostings = (List<ItemPostingViewModel>)this.ArchivePostings();
            ItemPostingViewModel editModeItemPostingVM = editModeArchivePostings.Find(x => x.Posting.Id == editId);
            editModeArchivePostings.RemoveAll(x => x.Posting.Id == editId);
            editModeItemPostingVM.Editing = true;
            editModeArchivePostings.Add(editModeItemPostingVM);
            return editModeArchivePostings;
        }

        public IEnumerable<ItemPostingViewModel> EditModeShopPostings(int id)
        {
            int editId = 5;
            List<ItemPostingViewModel> editModeShopPostings = new List<ItemPostingViewModel>();
            editModeShopPostings = (List<ItemPostingViewModel>)this.ShopPostings();
            ItemPostingViewModel editModeItemPostingVM = editModeShopPostings.Find(x => x.Posting.Id == editId);
            editModeShopPostings.RemoveAll(x => x.Posting.Id == editId);
            editModeItemPostingVM.Editing = true;
            editModeShopPostings.Add(editModeItemPostingVM);
            return editModeShopPostings;
        }

        public ItemPostingViewModel GetPosting(int id)
        {
            List<ItemPostingViewModel> postings = this.getAllPostings().ToList();
            return postings.Find(x => x.Posting.Id == id);
        }

        public IEnumerable<ItemPostingViewModel> ShopPostings()
        {
            List<ItemPostingViewModel> shopPostings = new List<ItemPostingViewModel>();
            shopPostings.Add(new ItemPostingViewModel()
            {
                Posting = new ItemPosting("4",
                    "some filepath 4",
                    "some title 4",
                    "some shortname 4",
                    "some header 4",
                    "some description 4",
                    "some size 4",
                    "some price 4"),
                Editing = false
            });
            shopPostings.Add(new ItemPostingViewModel()
            {
                Posting = new ItemPosting("5",
                    "some filepath 5",
                    "some title 5",
                    "some shortname 5",
                    "some header 5",
                    "some description 5",
                    "some size 5",
                    "some price 5"),
                Editing = false
            });
            shopPostings.Add(new ItemPostingViewModel()
            {
                Posting = new ItemPosting("6",
                    "some filepath 6",
                    "some title 6",
                    "some shortname 6",
                    "some header 6",
                    "some description 6",
                    "some size 6",
                    "some price 6"),
                Editing = false
            });
            return shopPostings;
        }
    }
}
