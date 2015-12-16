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
        private const int HTTP_SUCCESS = 200;
        private const int HTTP_INTERNAL_SERVER_ERROR = 500;
        private const int HTTP_BAD_REQUEST = 400;
        /*

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        The following section preceded the introduction of Simple Injector IoC. Simple Injector requires 
        one public constructor to exist but can be configured to restrict use of class as a singleton - see global.asax
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

        public ChangeResult DeletePictureFile(string filename, bool archive, bool display, string folder)
        {
            ChangeResult result = new ChangeResult();
            if (display)
            {
                // file is in database
                ItemPosting posting = repository.GetPosting(x => x.FileName.Normalise().ToUpper() == filename.Normalise().ToUpper(), archive);
                if (posting == null)
                {
                    // did not find filename - something's wrong, so don't delete file
                    result = new ChangeResult(false, "Error deleting " + filename + " from database", HTTP_INTERNAL_SERVER_ERROR);
                    Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(new Exception(result.Message)));
                    return result;
                }
                // remove from database
                result = repository.Delete(posting);
            }
            //return PictureFileRecordList(folder);
            // delete file
            File.Delete(Path.Combine(FullyMappedPictureFolder, filename));
            return new ChangeResult(true, "Deleted " + filename, HTTP_SUCCESS); ;
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
        public ChangeResult InsertPosting(PictureFileRecord pfr, bool archive)
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
                    result = new ChangeResult(false, "Cannot promote item which is at the top of the list", 400);
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

        public ChangeResult MovePicture(string filepath, bool archivedestination, bool displaydestination)
        {
            // filepaths / names brought in from ajax call will have %20 spaces and will not have JS escaped single quotes
            string filename = Utility.GetFilenameFromFilepath(filepath).Normalise();

            // default constructor gives failed results
            ChangeResult removeResult = new ChangeResult();
            ChangeResult insertResult = new ChangeResult();
            // initialising variables
            PictureFileRecord pfr = new PictureFileRecord(filepath);
            PictureFileRecord.StatusType source;
            List<ItemPostingViewModel> postingVMs = new List<ItemPostingViewModel>();

            // getting current database contents
            postingVMs.AddRange(ArchivePostings().ToList());
            postingVMs.AddRange(ShopPostings().ToList());
            // getting full info on item to move 
            ItemPostingViewModel moveItem = postingVMs.FirstOrDefault(x => x.ItemPosting.FileName.ToUpper().Normalise() == filename.ToUpper());
            ItemPosting posting = new ItemPosting();
            // determine current location of move item
            if (moveItem == null)
            {
                source = PictureFileRecord.StatusType.NotDisplayed;
            }
            else
            {
                posting = moveItem.ItemPosting;
                if (posting.Archive_Flag == true)
                {
                    source = PictureFileRecord.StatusType.Archived;
                }
                else
                {
                    source = PictureFileRecord.StatusType.ForSale;
                }
            }
            if (displaydestination)
            {
                // (1) the move item's being moved to the list it's already in
                if (archivedestination && source == PictureFileRecord.StatusType.Archived ||
                    !archivedestination && source == PictureFileRecord.StatusType.ForSale)
                {
                    return new ChangeResult(
                        false,
                        filename + " is already included in the " + ((archivedestination) ? "Archive" : "Home") + " page",
                        400);
                }
                else
                {
                    // (2) the moveItem's being moved from one list to another
                    if (archivedestination && source == PictureFileRecord.StatusType.ForSale ||
                        !archivedestination && source == PictureFileRecord.StatusType.Archived)
                    {
                        removeResult = RemoveFromDisplay(posting);
                        insertResult = InsertPosting(pfr, archivedestination);
                        return insertResult;
                    }
                    else
                    {
                        // (3) the moveItem's being moved onto a list from not_displayed
                        insertResult = InsertPosting(pfr, archivedestination);
                        return insertResult;
                    }
                }
            }
            // (4) the moveItem's being removed from all lists
            else
            {
                if (source == PictureFileRecord.StatusType.NotDisplayed)
                {
                    return new ChangeResult(
                        false,
                        filename + " is not currently displayed, and therefore is not in the database. Therefore it cannot be removed from the database",
                        500);
                }
                else
                {
                    removeResult = RemoveFromDisplay(posting);
                    return removeResult;
                }
            }
        }
    }
}