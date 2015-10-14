using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtPostings.Models
{
    public sealed class PostingService: IPostingService
    {
        // the service class is implemented as a singleton
        private static PostingService instance = null;
        private static readonly object padlock = new object();

        // explicit private constructor to prevent other classes creating instances
        private PostingService(IPostingRepository _repository) 
        {
            repository = _repository;
        }

        public static PostingService Instance
        {
            get
            {
                // ensures thread safety
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PostingService(new PostingRepository()); // site of injection?
                    }
                    return instance;
                }
            }
        }
        public string ShopText
        {
            get
            {
                return "Paintings are acrylic or oil. Click on any painting for a larger image. " +
                    "Please note, prices quoted for all items do not include postage and packing costs, " +
                    "which would be by arrangement. © Copyright Ann Chambers 2014"; 
            }
        }
        public string ArchiveText
        {
            get { return "Paintings are acrylic or oil. Click on any painting for a larger image. © Copyright Ann Chambers 2012";  }
        }
        private IPostingRepository repository;
        
        IEnumerable<ItemPosting> IPostingService.ShopPostings()
        {
            return repository.ShopPostings();
        }

        IEnumerable<ItemPosting> IPostingService.ArchivePostings()
        {
            return repository.ArchivePostings();
        }

        IEnumerable<ItemPosting> IPostingService.EditableShopPostings(int id)
        {
            IEnumerable<ItemPosting> postings = new List<ItemPosting>();
            postings = repository.ShopPostings();
            foreach (ItemPosting i in postings)
            {
                if (i.Id == id)
                {
                    i.Editing = true;
                }
            }            
            return postings;
        }
        
    }
}