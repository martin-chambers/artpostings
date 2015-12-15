using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace ArtPostings.Models
{
    public sealed class PostingService : IPostingService
    {
        /*

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        The following section preceded the introduction of Simple Injector IoC. Simple Injector requires 
        a public constructor to exist but can be configured to restrict use of class as a singleton - see global.asax
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // the service class is implemented as a singleton
        private static PostingService instance = null;
        private static readonly object padlock = new object();

        */

        private string webSafePictureFolder = ConfigurationManager.AppSettings["pictureLocation"];
        public string FullyMappedPictureFolder
        {
            get
            {
                string s;
                try
                {
                    s = HttpContext.Current.Server.MapPath("~/Content/Art");
                    if (s != null)
                    {
                        return s;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
                    return "";

                }
            }

        }


        /*

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        The following section preceded the introduction of Simple Injector IoC. Simple Injector requires 
        a public constructor to exist but can be configured to restrict use of class as a singleton - see global.asax
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
        */
        /// <summary>
        /// public constructor intended to be used with singleton configuration by DI framework
        /// </summary>
        /// <param name="_repository"></param>
        public PostingService(IPostingRepository _repository)
        {
            repository = _repository;
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
            get { return "Paintings are acrylic or oil. Click on any painting for a larger image. © Copyright Ann Chambers 2012"; }
        }
        private IPostingRepository repository;

        public IEnumerable<ItemPostingViewModel> ShopPostings()
        {
            IEnumerable<ItemPosting> postings = repository.ShopPostings();
            List<ItemPostingViewModel> postingViewModels = new List<ItemPostingViewModel>();
            foreach (ItemPosting posting in postings)
            {
                postingViewModels.Add(new ItemPostingViewModel() { ItemPosting = posting, Editing = false });
            }
            return postingViewModels;
        }

        public IEnumerable<ItemPostingViewModel> ArchivePostings()
        {
            IEnumerable<ItemPosting> postings = repository.ArchivePostings();
            List<ItemPostingViewModel> postingViewModels = new List<ItemPostingViewModel>();
            foreach (ItemPosting posting in postings)
            {
                postingViewModels.Add(new ItemPostingViewModel() { ItemPosting = posting, Editing = false });
            }
            return postingViewModels;
        }
        ItemPostingViewModel IPostingService.GetPosting(int id)
        {
            return new ItemPostingViewModel() { ItemPosting = repository.GetPosting(id), Editing = false };
        }
        /// <summary>
        /// Set the Editing property of the specified ItemPostingViewModel to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<ItemPostingViewModel> IPostingService.EditModeShopPostings(int id)
        {
            // the 'Editing' property of ItemPostingViewModel is originally set to false by the service
            List<ItemPostingViewModel> postings = new List<ItemPostingViewModel>();
            postings = this.ShopPostings().ToList();
            if (postings.Find(x => x.ItemPosting.Id == id) == null)
            {
                throw new ArgumentException("ItemPostingViewModel id not found", "id");
            }
            postings.Find(x => x.ItemPosting.Id == id).Editing = true;
            return postings;
        }
        IEnumerable<ItemPostingViewModel> IPostingService.EditModeArchivePostings(int id)
        {
            // the 'Editing' property of ItemPostingViewModel is originally set to false by the service
            List<ItemPostingViewModel> postings = new List<ItemPostingViewModel>();
            postings = this.ArchivePostings().ToList();
            if (postings.Find(x => x.ItemPosting.Id == id) == null)
            {
                throw new ArgumentException("ItemPostingViewModel id not found", "id");
            }
            postings.Find(x => x.ItemPosting.Id == id).Editing = true;
            return postings;
        }
        private ItemPosting extractSQLItemPosting(ItemPosting nonSQLPosting)
        {
            ItemPosting SQLPosting = new ItemPosting();
            SQLPosting.Id = nonSQLPosting.Id;
            SQLPosting.Description = Utility.NormaliseString(nonSQLPosting.Description);
            SQLPosting.FileName = Utility.NormaliseString(nonSQLPosting.FileName);
            SQLPosting.FilePath = Utility.NormaliseString(nonSQLPosting.FilePath);
            SQLPosting.Header = Utility.NormaliseString(nonSQLPosting.Header);
            SQLPosting.Price = Utility.NormaliseString(nonSQLPosting.Price);
            SQLPosting.ShortName = Utility.NormaliseString(nonSQLPosting.ShortName);
            SQLPosting.Size = Utility.NormaliseString(nonSQLPosting.Size);
            SQLPosting.Title = Utility.NormaliseString(nonSQLPosting.Title);
            SQLPosting.Order = nonSQLPosting.Order;
            return SQLPosting;
        }

        ChangeResult IPostingService.SaveShopChanges(ItemPostingViewModel vm)
        {
            ItemPosting sqlPosting = extractSQLItemPosting(vm.ItemPosting);
            ChangeResult result = repository.Update(sqlPosting, false);
            return result;
        }
        ChangeResult IPostingService.SaveArchiveChanges(ItemPostingViewModel vm)
        {
            ItemPosting posting = extractSQLItemPosting(vm.ItemPosting);
            ChangeResult result = repository.Update(posting, true);
            return result;
        }
        public List<PictureFileRecord> PictureFileRecordList(string mappedfolder, string status = "All")
        {
            string fullyMappedPictureFolder = mappedfolder;
            List<PictureFileRecord> pictureFiles = new List<PictureFileRecord>();
            List<ItemPostingViewModel> postings = new List<ItemPostingViewModel>();
            postings.AddRange(ShopPostings().ToList());
            postings.AddRange(ArchivePostings().ToList());
            List<string> files = new List<string>();
            files = Directory.GetFiles(fullyMappedPictureFolder).ToList();
            foreach (string file in files)
            {
                PictureFileRecord pictureFile = new PictureFileRecord(file);
                ItemPostingViewModel vm = postings.Find(x => x.ItemPosting.FileName.ToUpper() == HttpUtility.UrlPathEncode(pictureFile.FileName.ToUpper()));
                if (vm == null)
                {
                    pictureFile.Status = PictureFileRecord.StatusType.NotDisplayed;
                    pictureFile.Order = PictureFileRecord.NULL_ORDER.ToString(PictureFileRecord.NullStringFormat);
                    pictureFile.Header = "";

                }
                else
                {
                    pictureFile.Order = vm.ItemPosting.Order.ToString();
                    pictureFile.Header = vm.ItemPosting.Header;
                    pictureFile.Status = (vm.ItemPosting.Archive_Flag)
                        ? PictureFileRecord.StatusType.Archived
                        : PictureFileRecord.StatusType.ForSale;
                }
                PictureFileRecord.StatusType statusType = PictureFileRecord.GetStatusType(status);
                if (pictureFile.Status == statusType || statusType == PictureFileRecord.StatusType.All)
                {
                    pictureFiles.Add(pictureFile);
                }
            }
            return pictureFiles;
        }
        /// <summary>
        /// Removes an item from the database so it is no longer displayed on any list
        /// </summary>
        /// <param name="posting"></param>
        /// <returns></returns>
        public ChangeResult RemoveFromDisplay(ItemPosting posting)
        {
            return repository.Delete(posting);
        }

        public IEnumerable<PictureFileRecord> DeletePictureFile(string filename, bool archive, bool display, string folder)
        {
            File.Delete(Path.Combine(FullyMappedPictureFolder, filename));
            if (display)
            {
                ItemPosting posting = repository.GetPosting(x => x.FileName == filename, archive);
                repository.Delete(posting);
            }
            return PictureFileRecordList(folder);
        }
        ItemPostingViewModel IPostingService.CreateItemPostingViewModel(PictureFileRecord pfr)
        {
            ItemPosting posting = new ItemPosting();
            posting.FileName = Utility.GetFilenameFromFilepath(pfr.FilePath);
            posting.FilePath = pfr.FilePath;
            posting.Archive_Flag = true;
            posting.Description = "";
            posting.Header = "";
            posting.Order = 0;
            posting.Price = "";
            posting.ShortName = "";
            posting.Size = "";
            posting.Title = "";

            ItemPostingViewModel vm = new ItemPostingViewModel();
            vm.Editing = false;
            vm.ItemPosting = posting;
            return vm;
        }
        ChangeResult IPostingService.InsertPosting(PictureFileRecord pfr, bool archive)
        {
            // The constructor ItemPosting(string filename, bool archived) defaults the Order value to 0
            ItemPosting SQLPosting = extractSQLItemPosting(new ItemPosting(Utility.GetFilenameFromFilepath(pfr.FilePath), archive));
            ChangeResult result = repository.Create(SQLPosting, archive);
            return result;
        }

        /// <summary>
        /// Promotes the item in its list
        /// This action reduces the order number of the item by one if the order value is greater than zero. 
        /// The order value of the preceding item is increased by one.
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        ChangeResult IPostingService.AdvanceInList(PictureFileRecord rec)
        {
            ChangeResult result = new ChangeResult();
            int order;
            bool archive = rec.Status == PictureFileRecord.StatusType.Archived;
            string encodedFilename = HttpUtility.UrlPathEncode(rec.FileName);
            ItemPosting subjectItem = repository.GetPosting(x => x.FileName.ToUpper() == encodedFilename.ToUpper(), archive);
            try
            {
                order = Convert.ToInt32(subjectItem.Order);
                if (order > 0)
                {
                    // get previously ordered item
                    ItemPosting precedingItem = repository.GetPosting(pre => pre.Order == subjectItem.Order - 1, archive);
                    result = repository.ExchangeOrders(subjectItem, precedingItem, archive);
                }
                else
                {
                    result = new ChangeResult(false, "Cannot promote item which is at the top of the list");
                }
            }
            catch (Exception anEx)
            {
                // log but don't halt execution - the javascript function has fired, most likely because 
                // of difficult-to-understand interaction between the paging control and onclick event in webgrid
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(anEx));
            }
            return result;
        }
    }
}