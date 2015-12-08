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
        // the service class is implemented as a singleton
        private static PostingService instance = null;
        private static readonly object padlock = new object();

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
        public ChangeResult RemoveFromDisplay(ItemPosting posting)
        {
            return repository.Delete(posting);
        }

        public IEnumerable<PictureFileRecord> DeletePictureFile(string filename, string folder)
        {
            File.Delete(Path.Combine(FullyMappedPictureFolder, filename));
            repository.Delete(new ItemPosting(filename));
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

            ItemPosting SQLPosting = extractSQLItemPosting(new ItemPosting(Utility.GetFilenameFromFilepath(pfr.FilePath), archive));
            ChangeResult result = repository.Create(SQLPosting, archive);
            return result;
        }
    }
}