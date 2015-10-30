using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace ArtPostings.Models
{
    public sealed class PostingService: IPostingService
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
                catch(Exception ex)
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
            get { return "Paintings are acrylic or oil. Click on any painting for a larger image. © Copyright Ann Chambers 2012";  }
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
            if(postings.Find(x=>x.ItemPosting.Id==id) == null)
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
        private ItemPosting extractItemPostingVM(ItemPostingViewModel vm)
        {
            ItemPosting ip = new ItemPosting();
            ip.Id = vm.ItemPosting.Id;
            ip.Description = Utility.PrepForSql(vm.ItemPosting.Description);
            ip.FileName = Utility.PrepForSql(vm.ItemPosting.FileName);
            ip.FilePath = Utility.PrepForSql(vm.ItemPosting.FilePath);
            ip.Header = Utility.PrepForSql(vm.ItemPosting.Header);
            ip.Price = Utility.PrepForSql(vm.ItemPosting.Price);
            ip.ShortName = Utility.PrepForSql(vm.ItemPosting.ShortName);
            ip.Size = Utility.PrepForSql(vm.ItemPosting.Size);
            ip.Title = Utility.PrepForSql(vm.ItemPosting.Title);
            return ip;
        }

        void IPostingService.SaveShopChanges(ItemPostingViewModel vm)
        {
            ItemPosting posting = extractItemPostingVM(vm);
            repository.Update(posting, false);
        }
        void IPostingService.SaveArchiveChanges(ItemPostingViewModel vm)
        {
            ItemPosting posting = vm.ItemPosting;
            repository.Update(posting, true);
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
                ItemPostingViewModel vm = postings.Find(x => x.ItemPosting.FileName.ToUpper() == pictureFile.FileName.ToUpper());
                if(vm == null)
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
        public IEnumerable<PictureFileRecord> DeletePictureFile(string filename, string folder)
        {
            File.Delete(Path.Combine(FullyMappedPictureFolder, filename));
            return PictureFileRecordList(folder);
        }

    }
}