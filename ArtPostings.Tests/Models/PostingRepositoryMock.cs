using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtPostings.Controllers;
using ArtPostings.Models;

namespace ArtPostings.Tests.Models
{
    public class PostingRepositoryMock : IPostingRepository
    {
        public IEnumerable<ItemPosting> ArchivePostings()
        {
            List<ItemPosting> archivePostings = new List<ItemPosting>();
            archivePostings.Add(new ItemPosting("1",
                    "1",
                    "filepath 1",
                    "filename 1",
                    "title 1",
                    "shortname 1",
                    "header 1",
                    "description 1",
                    "size 1",
                    "price 1",
                    "true"));
            archivePostings.Add(new ItemPosting("2",
                    "2",
                    "filepath 2",
                    "filename 2",
                    "title 2",
                    "shortname 2",
                    "header 2",
                    "description 2",
                    "size 2",
                    "price 2",
                    "true"));
            archivePostings.Add(new ItemPosting("3",
                    "3",
                    "filepath 3",
                    "filename 3",
                    "title 3",
                    "shortname 3",
                    "header 3",
                    "description 3",
                    "size 3",
                    "price 3",
                    "true"));
            return archivePostings;
        }

        public bool Create(ItemPosting posting)
        {
            throw new NotImplementedException();
        }

        public ChangeResult Create(ItemPosting posting, bool archive)
        {
            throw new NotImplementedException();
        }

        public ChangeResult Delete(ItemPosting posting)
        {
            throw new NotImplementedException();
        }

        public ItemPosting GetPosting(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemPosting> ShopPostings()
        {
            List<ItemPosting> shopPostings = new List<ItemPosting>();
            shopPostings.Add(new ItemPosting("4",
                "4",
                "filepath 4",
                "filename 4",
                "title 4",
                "shortname 4",
                "header 4",
                "description 4",
                "size 4",
                "price 4",
                "false"));
            shopPostings.Add(new ItemPosting("5",
                "5",
                "filepath 5",
                "filename 5",
                "title 5",
                "shortname 5",
                "header 5",
                "description 5",
                "size 5",
                "price 5",
                "false"));
            shopPostings.Add(new ItemPosting("6",
                "6",
                "filepath 6",
                "filename 6",
                "title 6",
                "shortname 6",
                "header 6",
                "description 6",
                "size 6",
                "price 6",
                "false"));
            return shopPostings;
        }

    public void Update(ItemPosting posting, bool archived)
        {
            throw new NotImplementedException();
        }
        
        ChangeResult IPostingRepository.Update(ItemPosting posting, bool archived)
        {
            throw new NotImplementedException();
        }
    }
}
