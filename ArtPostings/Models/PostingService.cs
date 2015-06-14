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
        private IPostingRepository repository;
        
        IEnumerable<ItemPosting> IPostingService.ShopPostings()
        {
            return repository.ShopPostings();
        }

        IEnumerable<ItemPosting> IPostingService.ArchivePostings()
        {
            return repository.ArchivePostings();
        }
    }
}