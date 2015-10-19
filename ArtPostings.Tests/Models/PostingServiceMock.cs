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
        private IEnumerable<ItemPosting> getAllPostings()
        {
            List<ItemPosting> postings = new List<ItemPosting>();
            postings.AddRange(this.ArchivePostings());
            postings.AddRange(this.ShopPostings());
            return postings;
        }

        public IEnumerable<ItemPosting> ArchivePostings()
        {
            List<ItemPosting> archivePostings = new List<ItemPosting>();
            archivePostings.Add(
                new ItemPosting(
                "1",
                "some filepath",
                "some title",
                "some shortname",
                "some header",
                "some description",
                "some size",
                "some price"
                ));
            archivePostings.Add(
                new ItemPosting(
                "2",
                "some filepath 2",
                "some title 2",
                "some shortname 2",
                "some header 2",
                "some description 2",
                "some size 2",
                "some price 2"
                ));
            archivePostings.Add(
                new ItemPosting(
                "3",
                "some filepath 3",
                "some title 3",
                "some shortname 3",
                "some header 3",
                "some description 3",
                "some size 3",
                "some price 3"
                ));
            return archivePostings;
        }

        public IEnumerable<ItemPosting> EditModeArchivePostings(int id)
        {
            int editId = 2;
            List<ItemPosting> editModeArchivePostings = new List<ItemPosting>();
            editModeArchivePostings = (List<ItemPosting>)this.ArchivePostings();
            ItemPosting editModeItemPosting = editModeArchivePostings.Find(x => x.Id == editId);
            editModeArchivePostings.RemoveAll(x => x.Id == editId);
            editModeItemPosting.Editing = true;
            editModeArchivePostings.Add(editModeItemPosting);
            return editModeArchivePostings;
        }

        public IEnumerable<ItemPosting> EditModeShopPostings(int id)
        {
            int editId = 5;
            List<ItemPosting> editModeShopPostings = new List<ItemPosting>();
            editModeShopPostings = (List<ItemPosting>)this.ShopPostings();
            ItemPosting editModeItemPosting = editModeShopPostings.Find(x => x.Id == editId);
            editModeShopPostings.RemoveAll(x => x.Id == editId);
            editModeItemPosting.Editing = true;
            editModeShopPostings.Add(editModeItemPosting);
            return editModeShopPostings;
        }

        public ItemPosting GetPosting(int id)
        {
            List<ItemPosting> postings = this.getAllPostings().ToList();
            return postings.Find(x => x.Id == id);
        }

        public IEnumerable<ItemPosting> ShopPostings()
        {
            List<ItemPosting> shopPostings = new List<ItemPosting>();
            shopPostings.Add(
                new ItemPosting(
                "4",
                "some filepath",
                "some title",
                "some shortname",
                "some header",
                "some description",
                "some size",
                "some price"
                ));
            shopPostings.Add(
                new ItemPosting(
                "5",
                "some filepath 2",
                "some title 2",
                "some shortname 2",
                "some header 2",
                "some description 2",
                "some size 2",
                "some price 2"
                ));
            shopPostings.Add(
                new ItemPosting(
                "6",
                "some filepath 3",
                "some title 3",
                "some shortname 3",
                "some header 3",
                "some description 3",
                "some size 3",
                "some price 3"
                ));
            return shopPostings;
        }
    }
}
